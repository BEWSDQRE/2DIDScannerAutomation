using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Keyence.AutoID.SDK;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace FirstStepApp
{
    public partial class Form1 : Form
    { 
        private ReaderAccessor m_reader = new ReaderAccessor();
        private ReaderSearcher m_searcher = new ReaderSearcher();
        List<NicSearchResult> m_nicList = new List<NicSearchResult>();
        
        // Excel file variables
        private string m_excelFilePath = "";
        private int m_currentRow = 1;

        // Duplicate detection - stores all scanned 2DIDs
        private HashSet<string> m_scannedData = new HashSet<string>();
        private int m_duplicateCount = 0;

        // Hardcoded IP addresses for auto-connect
        private const string TARGET_NIC_IP = "192.168.100.11";
        private const string TARGET_READER_IP = "192.168.100.100";
        private bool m_autoConnecting = false;

        // Tray Mode variables
        private bool m_isTrayMode = false;
        private int m_trayRows = 0;
        private int m_trayCols = 0;
        private int m_currentTrayRow = 1;
        private int m_currentTrayCol = 1;
        private string[,] m_trayData = null;  // Stores scanned data for tray grid
        private Panel m_gridPanel = null;     // Panel for mini-grid display

        public Form1()
        {
            InitializeComponent();
            
            // Set window title with version number
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = $"FirstStepApp - 2D Barcode Scanner  v{version.Major}.{version.Minor}.{version.Build}";
            
            m_nicList = m_searcher.ListUpNic();
            if (m_nicList != null)
            {
                for (int i = 0; i < m_nicList.Count; i++)
                {
                    NICcomboBox.Items.Add(m_nicList[i].NicIpAddr);
                }
            }
            NICcomboBox.SelectedIndex = 0;

            // Enable form-level key capture for foot pedal support
            this.KeyPreview = true;
            this.KeyPress += Form1_KeyPress;

            // Auto-connect on form load
            this.Load += Form1_Load;
        }

        // Foot pedal / keyboard shortcut support
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '`' && TgrBtn.Enabled)
            {
                // Backtick (`) triggers the scanner
                TgrBtn_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.KeyChar == '-' && btnSkipCell.Visible && btnSkipCell.Enabled)
            {
                // Minus (-) skips the current cell
                btnSkipCell_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.KeyChar == '=' && btnNextRow.Visible && btnNextRow.Enabled)
            {
                // Equals (=) skips to the next row
                btnNextRow_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Ensure Step 3 controls are on top of liveview
            lblStep3.BringToFront();
            DataText.BringToFront();
            lblFooter.BringToFront();
            btnHelp.BringToFront();
            lblStatus.BringToFront();
            
            // Start auto-connect process
            AutoConnect();
        }

        private void AutoConnect()
        {
            m_autoConnecting = true;
            lblStatus.Text = "Status: Auto-connecting...\n\nSearching for scanner...";
            lblNicHint.Text = "Searching for scanner...";
            lblNicHint.ForeColor = System.Drawing.Color.Blue;

            // Step 1: Select the target NIC (192.168.100.11)
            int nicIndex = -1;
            for (int i = 0; i < m_nicList.Count; i++)
            {
                if (m_nicList[i].NicIpAddr == TARGET_NIC_IP)
                {
                    nicIndex = i;
                    break;
                }
            }

            if (nicIndex >= 0)
            {
                NICcomboBox.SelectedIndex = nicIndex;
            }
            else
            {
                // Target NIC not found, use first available
                lblStatus.Text = $"Status: NIC {TARGET_NIC_IP} not found.\nUsing default NIC.\n\nSearching...";
            }

            // Step 2: Start searching for readers
            if (!m_searcher.IsSearching)
            {
                m_searcher.SelectedNicSearchResult = m_nicList[NICcomboBox.SelectedIndex];
                NICcomboBox.Enabled = false;
                SchBtn.Enabled = false;
                SctBtn.Enabled = false;
                comboBox1.Items.Clear();

                // Start searching readers with auto-select callback
                m_searcher.Start((res) =>
                {
                    BeginInvoke(new delegateUserControl(AutoSearchListUp), res.IpAddress);
                });
            }
        }

        private void AutoSearchListUp(string ip)
        {
            if (ip != "")
            {
                comboBox1.Items.Add(ip);
                comboBox1.SelectedIndex = comboBox1.Items.Count - 1;

                // Check if this is the target reader IP and auto-connect
                if (m_autoConnecting && ip == TARGET_READER_IP)
                {
                    // Found the target reader, auto-select it
                    lblNicHint.Text = $"Found {TARGET_READER_IP}! Connecting...";
                    lblStatus.Text = $"Status: Found scanner!\n{TARGET_READER_IP}\n\nConnecting...";
                }
                return;
            }
            else
            {
                // Search completed
                NICcomboBox.Enabled = true;
                SctBtn.Enabled = true;
                SchBtn.Enabled = true;

                // Auto-select the target reader if found
                if (m_autoConnecting)
                {
                    int readerIndex = -1;
                    for (int i = 0; i < comboBox1.Items.Count; i++)
                    {
                        if (comboBox1.Items[i].ToString() == TARGET_READER_IP)
                        {
                            readerIndex = i;
                            break;
                        }
                    }

                    if (readerIndex >= 0)
                    {
                        comboBox1.SelectedIndex = readerIndex;
                        // Simulate clicking the Select button
                        SctBtn.Checked = true;
                        lblNicHint.Text = $"Connected to {TARGET_READER_IP}";
                        lblNicHint.ForeColor = System.Drawing.Color.Green;
                        lblStatus.Text = $"Status: Connected!\n{TARGET_READER_IP}\n\nReady to scan.\nCreate Excel file first.";
                    }
                    else
                    {
                        lblNicHint.Text = $"Scanner not found!";
                        lblNicHint.ForeColor = System.Drawing.Color.Red;
                        lblStatus.Text = $"Status: Scanner not found\n{TARGET_READER_IP}\n\nPlease check connection.";
                    }
                    m_autoConnecting = false;
                }
            }
        }

        private void SchBtn_Click(object sender, EventArgs e)
        {           
            //m_searcher.IsSearching is true while searching readers.
            if (!m_searcher.IsSearching)
            {
                m_searcher.SelectedNicSearchResult = m_nicList[NICcomboBox.SelectedIndex];
                NICcomboBox.Enabled = false;
                SchBtn.Enabled = false;
                SctBtn.Enabled = false;
                comboBox1.Items.Clear();
                //Start searching readers.
                m_searcher.Start((res) =>
                {
                    //Define searched actions here.Defined actions work asynchronously.
                    //"SearchListUp" works when a reader was searched.
                    BeginInvoke(new delegateUserControl(SearchListUp), res.IpAddress);
                });
            }
        }
        private void SctBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (SctBtn.Checked)
            {
                if (comboBox1.SelectedItem != null)
                {
                    //Stop liveview.
                    liveviewForm1.EndReceive();
                    //Set ip address of liveview.
                    liveviewForm1.IpAddress = comboBox1.SelectedItem.ToString();
                    //Start liveview.
                    liveviewForm1.BeginReceive();
                    //Set ip address of ReaderAccessor.
                    m_reader.IpAddress = comboBox1.SelectedItem.ToString();
                    //Connect TCP/IP.
                    m_reader.Connect((data) =>
                    {
                        //Define received data actions here.Defined actions work asynchronously.
                        //"ReceivedDataWrite" works when reading data was received.
                        BeginInvoke(new delegateUserControl(ReceivedDataWrite), Encoding.ASCII.GetString(data));
                    });
                    NICcomboBox.Enabled = false;
                    SchBtn.Enabled = false;
                    comboBox1.Enabled = false;
                    TgrBtn.Enabled = true;
                }
            }
            else
            {
                NICcomboBox.Enabled = true;
                SchBtn.Enabled = true;
                comboBox1.Enabled = true;
                TgrBtn.Enabled = false;
            }
        }
        private void TgrBtn_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem != null)
            {
                //ExecCommand("LON") triggers the scanner and returns the scanned data.
                ReceivedDataWrite(m_reader.ExecCommand("LON"));
            }
        }
        //delegateUserControl is for controlling UserControl from other threads.
        private delegate void delegateUserControl(string str);
        private void ReceivedDataWrite(string receivedData)
        {
            // Update the text field first so the user can see the scanned data
            DataText.Text=("[" + m_reader.IpAddress + "][" + DateTime.Now + "]" + receivedData);
            
            // Force the UI to repaint immediately so the user sees the result
            // before the Excel save operation (which can take time and block the UI thread)
            DataText.Refresh();
            lblStatus.Refresh();
            Application.DoEvents();
            
            // Auto-save to Excel if file is created and data is not empty
            AutoSaveToExcel(receivedData);
        }
        private void SearchListUp(string ip)
        {
            if (ip != "")
            {
                comboBox1.Items.Add(ip);
                comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
                return;
            }
            else
            {
                NICcomboBox.Enabled = true;
                SctBtn.Enabled = true;
                SchBtn.Enabled = true;
            }
        }
        //Dispose before closing Form for avoiding error.
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Turn off the scanner light (LOFF) before closing
                if (m_reader != null && !string.IsNullOrEmpty(m_reader.IpAddress))
                {
                    m_reader.ExecCommand("LOFF");
                }
            }
            catch
            {
                // Ignore errors during cleanup
            }

            m_reader.Dispose();
            m_searcher.Dispose();
            liveviewForm1.Dispose();
        }

        // Create a new Excel file
        private void btnCreateFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFileName.Text))
            {
                MessageBox.Show("Please enter a file name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Open folder browser to select save location
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                saveDialog.FileName = txtFileName.Text;
                saveDialog.Title = "Save Excel File";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    m_excelFilePath = saveDialog.FileName;

                    if (m_isTrayMode)
                    {
                        // Create Tray Mode Excel file
                        CreateTrayExcelFile();
                    }
                    else
                    {
                        // Create Loose Unit Mode Excel file
                        CreateLooseUnitExcelFile();
                    }
                }
            }
        }

        // Create Excel file for Loose Unit Mode
        private void CreateLooseUnitExcelFile()
        {
            try
            {
                // Create a new Excel workbook with headers
                FileInfo fileInfo = new FileInfo(m_excelFilePath);
                using (var package = new ExcelPackage(fileInfo))
                {
                    var worksheet = package.Workbook.Worksheets.Add("ScanData");
                    
                    // Create header row with column titles
                    worksheet.Cells[1, 1].Value = "No.";
                    worksheet.Cells[1, 2].Value = "Scan Data";
                    worksheet.Cells[1, 3].Value = "IP Address";
                    worksheet.Cells[1, 4].Value = "Timestamp";
                    
                    // Style headers
                    using (var headerRange = worksheet.Cells["A1:D1"])
                    {
                        headerRange.Style.Font.Bold = true;
                        headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }
                    
                    // Set column widths
                    worksheet.Column(1).Width = 6;   // No.
                    worksheet.Column(2).Width = 35;  // Scan Data
                    worksheet.Column(3).Width = 18;  // IP Address
                    worksheet.Column(4).Width = 22;  // Timestamp
                    
                    package.Save();
                }

                m_currentRow = 2; // Start data from row 2 (row 1 is header)
                
                // Reset duplicate tracking for new file
                m_scannedData.Clear();
                m_duplicateCount = 0;
                
                // Reset tray controls visibility
                btnSkipCell.Visible = false;
                btnNextRow.Visible = false;
                lblCurrentPos.Visible = false;
                pnlTrayGrid.Visible = false;
                TgrBtn.Visible = true;
                TgrBtn.Size = new System.Drawing.Size(340, 26);
                TgrBtn.Location = new System.Drawing.Point(11, 116);
                
                // Adjust liveview position for Loose Unit mode (directly below trigger button)
                liveviewForm1.Location = new System.Drawing.Point(11, 150);
                liveviewForm1.Size = new System.Drawing.Size(340, 180);
                
                // Reset Step 3 and DataText positions for Loose Unit mode
                lblStep3.Location = new System.Drawing.Point(9, 338);
                DataText.Location = new System.Drawing.Point(11, 358);
                lblFooter.Location = new System.Drawing.Point(9, 385);
                
                // Bring Step 3 controls to front
                lblStep3.BringToFront();
                DataText.BringToFront();
                lblFooter.BringToFront();
                
                lblStatus.Text = $"Status: File created\n{Path.GetFileName(m_excelFilePath)}\n\nAuto-save enabled!\nDuplicate detection ON!\n\nScans will be saved automatically.\nNext row: {m_currentRow}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Auto-save scan data to Excel - routes to correct mode
        private void AutoSaveToExcel(string receivedData)
        {
            if (m_isTrayMode)
            {
                AutoSaveToTray(receivedData);
            }
            else
            {
                AutoSaveToLooseUnit(receivedData);
            }
        }

        // Auto-save for Loose Unit Mode
        private void AutoSaveToLooseUnit(string receivedData)
        {
            // Check if Excel file is created
            if (string.IsNullOrEmpty(m_excelFilePath) || !File.Exists(m_excelFilePath))
            {
                // No file created yet, just skip auto-save silently
                return;
            }

            // Check if received data is empty or null
            string rawData = receivedData?.Trim();
            if (string.IsNullOrWhiteSpace(rawData))
            {
                // Empty scan - don't save, update status
                lblStatus.Text = $"Status: Empty scan skipped\n{Path.GetFileName(m_excelFilePath)}\n\nNo data detected.\nNext row: {m_currentRow}";
                return;
            }

            // Check for duplicate scan
            if (m_scannedData.Contains(rawData))
            {
                m_duplicateCount++;
                // Alert user about duplicate
                lblStatus.Text = $"Status: DUPLICATE!\n{Path.GetFileName(m_excelFilePath)}\n\nData: {rawData}\n\nThis 2DID was already scanned!\nNot saved to Excel.\n\nDuplicates blocked: {m_duplicateCount}";
                lblStatus.ForeColor = Color.Red;
                
                // Flash the data text box to highlight the duplicate
                DataText.BackColor = Color.LightCoral;
                
                // Reset color after a short delay using a timer
                Timer resetTimer = new Timer();
                resetTimer.Interval = 1500; // 1.5 seconds
                resetTimer.Tick += (s, args) =>
                {
                    DataText.BackColor = SystemColors.Control;
                    lblStatus.ForeColor = SystemColors.ControlText;
                    resetTimer.Stop();
                    resetTimer.Dispose();
                };
                resetTimer.Start();
                
                return;
            }

            // Add to duplicate tracking BEFORE saving to Excel
            // This prevents the race where data is saved but UI hasn't updated,
            // causing the user to scan again and trigger a false duplicate
            m_scannedData.Add(rawData);

            // Show immediate feedback that scan was accepted
            lblStatus.Text = $"Status: Saving...\n{Path.GetFileName(m_excelFilePath)}\n\nRow: {m_currentRow}\nData: {rawData}\n\nUnique scans: {m_scannedData.Count}";
            lblStatus.Refresh();

            try
            {
                FileInfo fileInfo = new FileInfo(m_excelFilePath);
                using (var package = new ExcelPackage(fileInfo))
                {
                    var worksheet = package.Workbook.Worksheets[1];
                    
                    // Save data in separate columns
                    int rowNumber = m_currentRow - 1; // Row number (1, 2, 3...)
                    worksheet.Cells[m_currentRow, 1].Value = rowNumber;           // No.
                    worksheet.Cells[m_currentRow, 2].Value = rawData;             // Scan Data only
                    worksheet.Cells[m_currentRow, 3].Value = m_reader.IpAddress;  // IP Address
                    worksheet.Cells[m_currentRow, 4].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Timestamp
                    
                    package.Save();
                }

                lblStatus.Text = $"Status: Auto-saved!\n{Path.GetFileName(m_excelFilePath)}\n\nRow: {m_currentRow}\nData: {rawData}\n\nUnique scans: {m_scannedData.Count}\nNext row: {m_currentRow + 1}";
                m_currentRow++;
            }
            catch (Exception ex)
            {
                // Remove from tracking if save failed so user can retry
                m_scannedData.Remove(rawData);
                lblStatus.Text = $"Status: Save error!\n{ex.Message}";
            }
        }

        // Extract raw scan data from the formatted string [IP][DateTime]Data
        private string ExtractRawScanData(string formattedData)
        {
            if (string.IsNullOrEmpty(formattedData))
                return string.Empty;

            // The format is: [IP][DateTime]ActualData
            // Find the last ']' and get everything after it
            int lastBracket = formattedData.LastIndexOf(']');
            if (lastBracket >= 0 && lastBracket < formattedData.Length - 1)
            {
                // Get the data after the last bracket and trim whitespace/newlines
                string rawData = formattedData.Substring(lastBracket + 1).Trim();
                return rawData;
            }
            
            // If no brackets found, return the trimmed original data
            return formattedData.Trim();
        }

        // Mode selection event handlers
        private void rbLooseUnit_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLooseUnit.Checked)
            {
                m_isTrayMode = false;
                pnlTrayConfig.Visible = false;
                btnSkipCell.Visible = false;
                btnNextRow.Visible = false;
                lblCurrentPos.Visible = false;
                pnlTrayGrid.Visible = false;
                
                // Reset button and liveview positions for Loose Unit mode
                TgrBtn.Visible = true;
                TgrBtn.Size = new System.Drawing.Size(340, 26);
                TgrBtn.Location = new System.Drawing.Point(11, 116);
                liveviewForm1.Location = new System.Drawing.Point(11, 150);
                liveviewForm1.Size = new System.Drawing.Size(340, 180);
                
                // Reset Step 3 and DataText positions for Loose Unit mode
                lblStep3.Location = new System.Drawing.Point(9, 338);
                DataText.Location = new System.Drawing.Point(11, 358);
                lblFooter.Location = new System.Drawing.Point(9, 385);
                
                // Bring Step 3 controls to front
                lblStep3.BringToFront();
                DataText.BringToFront();
                lblFooter.BringToFront();
                
                // Reset form size (narrower, no tray grid)
                this.ClientSize = new System.Drawing.Size(545, 440);
                
                lblStatus.Text = "Status: Loose Unit Mode\n\nWorkflow:\n1. Create Excel file\n2. Scanner auto-connects\n3. Scan and save";
            }
        }

        private void rbTray_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTray.Checked)
            {
                m_isTrayMode = true;
                pnlTrayConfig.Visible = true;
                
                // Hide tray-specific controls until file is created
                btnSkipCell.Visible = false;
                btnNextRow.Visible = false;
                lblCurrentPos.Visible = false;
                pnlTrayGrid.Visible = false;
                
                // Keep same button/liveview layout as Loose Unit until file is created
                TgrBtn.Visible = true;
                TgrBtn.Size = new System.Drawing.Size(340, 26);
                TgrBtn.Location = new System.Drawing.Point(11, 116);
                liveviewForm1.Location = new System.Drawing.Point(11, 150);
                liveviewForm1.Size = new System.Drawing.Size(340, 180);
                
                // Keep Step 3 and DataText positions same as Loose Unit until file is created
                lblStep3.Location = new System.Drawing.Point(9, 338);
                DataText.Location = new System.Drawing.Point(11, 358);
                lblFooter.Location = new System.Drawing.Point(9, 385);
                
                // Bring Step 3 controls to front
                lblStep3.BringToFront();
                DataText.BringToFront();
                lblFooter.BringToFront();
                
                // Expand form to show tray grid (wider, same height as Loose Unit)
                this.ClientSize = new System.Drawing.Size(880, 440);
                
                lblStatus.Text = "Status: Tray Mode\n\nWorkflow:\n1. Set tray dimensions\n2. Create Excel file\n3. Scanner auto-connects\n4. Scan row by row";
            }
        }

        // Create Excel file for Tray Mode
        private void CreateTrayExcelFile()
        {
            m_trayRows = (int)numTrayRows.Value;
            m_trayCols = (int)numTrayCols.Value;
            m_currentTrayRow = 1;
            m_currentTrayCol = 1;
            m_trayData = new string[m_trayRows, m_trayCols];
            
            try
            {
                FileInfo fileInfo = new FileInfo(m_excelFilePath);
                using (var package = new ExcelPackage(fileInfo))
                {
                    var worksheet = package.Workbook.Worksheets.Add("TrayData");
                    
                    // Create corner cell (Row/Col label)
                    worksheet.Cells[1, 1].Value = "Row/Col";
                    worksheet.Cells[1, 1].Style.Font.Bold = true;
                    worksheet.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Column(1).Width = 8;
                    
                    // Create column headers (1, 2, 3, ...) - starting from column 2
                    for (int col = 1; col <= m_trayCols; col++)
                    {
                        worksheet.Cells[1, col + 1].Value = col;
                        worksheet.Cells[1, col + 1].Style.Font.Bold = true;
                        worksheet.Cells[1, col + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[1, col + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        worksheet.Cells[1, col + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Column(col + 1).Width = 12;
                    }
                    
                    // Create row headers (1, 2, 3, ...) - starting from row 2
                    for (int row = 1; row <= m_trayRows; row++)
                    {
                        worksheet.Cells[row + 1, 1].Value = row;
                        worksheet.Cells[row + 1, 1].Style.Font.Bold = true;
                        worksheet.Cells[row + 1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[row + 1, 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        worksheet.Cells[row + 1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                    
                    package.Save();
                }

                // Reset duplicate tracking
                m_scannedData.Clear();
                m_duplicateCount = 0;
                
                // Show tray controls - Trigger button full width below Scanner not found label
                TgrBtn.Visible = true;
                TgrBtn.Size = new System.Drawing.Size(340, 26);
                TgrBtn.Location = new System.Drawing.Point(11, 116);
                
                // Skip Cell and Next Row buttons below Trigger button
                btnSkipCell.Visible = true;
                btnNextRow.Visible = true;
                lblCurrentPos.Visible = true;
                btnSkipCell.Enabled = true;
                btnNextRow.Enabled = true;
                btnSkipCell.Location = new System.Drawing.Point(11, 148);
                btnNextRow.Location = new System.Drawing.Point(186, 148);
                
                // Adjust liveview position for Tray mode (below the extra row of buttons)
                liveviewForm1.Location = new System.Drawing.Point(11, 180);
                liveviewForm1.Size = new System.Drawing.Size(340, 180);
                
                // Adjust Step 3 and DataText positions for Tray mode (liveview moves down 30px)
                lblStep3.Location = new System.Drawing.Point(9, 368);
                DataText.Location = new System.Drawing.Point(11, 388);
                lblFooter.Location = new System.Drawing.Point(9, 415);
                
                // Bring Step 3 controls to front
                lblStep3.BringToFront();
                DataText.BringToFront();
                lblFooter.BringToFront();
                
                // Expand form height for tray mode with extra buttons
                this.ClientSize = new System.Drawing.Size(880, 440);
                
                // Show tray grid panel
                pnlTrayGrid.Visible = true;
                
                // Create visual mini-grid
                CreateMiniGrid();
                
                UpdateTrayPosition();
                
                lblStatus.Text = $"Status: Tray file created\n{Path.GetFileName(m_excelFilePath)}\n\nTray: {m_trayRows} x {m_trayCols}\nTotal cells: {m_trayRows * m_trayCols}\n\nStart scanning!";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating tray file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Create the visual mini-grid
        private void CreateMiniGrid()
        {
            pnlTrayGrid.Controls.Clear();
            
            // Fixed cell size - does not change with window resize
            int cellWidth = 12;
            int cellHeight = 12;
            
            // Calculate total grid size (may be larger than panel - that's OK, panel will scroll)
            int gridWidth = m_trayCols * cellWidth + 2;
            int gridHeight = m_trayRows * cellHeight + 2;
            
            m_gridPanel = new Panel();
            m_gridPanel.Location = new System.Drawing.Point(5, 5);
            m_gridPanel.Size = new System.Drawing.Size(gridWidth, gridHeight);
            m_gridPanel.BorderStyle = BorderStyle.FixedSingle;
            
            for (int row = 0; row < m_trayRows; row++)
            {
                for (int col = 0; col < m_trayCols; col++)
                {
                    Panel cell = new Panel();
                    cell.Size = new System.Drawing.Size(cellWidth - 1, cellHeight - 1);
                    cell.Location = new System.Drawing.Point(col * cellWidth, row * cellHeight);
                    cell.BackColor = Color.LightGray;
                    cell.BorderStyle = BorderStyle.FixedSingle;
                    cell.Name = $"cell_{row}_{col}";
                    m_gridPanel.Controls.Add(cell);
                }
            }
            
            pnlTrayGrid.Controls.Add(m_gridPanel);
            
            // Add legend
            Label lblLegend = new Label();
            lblLegend.Text = "Gray=Pending  Green=Scanned  Yellow=Current  Red=Skipped";
            lblLegend.Location = new System.Drawing.Point(5, m_gridPanel.Bottom + 10);
            lblLegend.AutoSize = true;
            lblLegend.Font = new Font("Microsoft Sans Serif", 7F);
            pnlTrayGrid.Controls.Add(lblLegend);
            
            // Highlight current cell
            UpdateGridHighlight();
        }

        // Update the mini-grid to highlight current position
        private void UpdateGridHighlight()
        {
            if (m_gridPanel == null) return;
            
            for (int row = 0; row < m_trayRows; row++)
            {
                for (int col = 0; col < m_trayCols; col++)
                {
                    Panel cell = m_gridPanel.Controls[$"cell_{row}_{col}"] as Panel;
                    if (cell != null)
                    {
                        if (row == m_currentTrayRow - 1 && col == m_currentTrayCol - 1)
                        {
                            // Current cell - yellow
                            cell.BackColor = Color.Yellow;
                        }
                        else if (m_trayData != null && !string.IsNullOrEmpty(m_trayData[row, col]))
                        {
                            if (m_trayData[row, col] == "[SKIPPED]")
                            {
                                // Skipped cell - red
                                cell.BackColor = Color.LightCoral;
                            }
                            else
                            {
                                // Scanned cell - green
                                cell.BackColor = Color.LightGreen;
                            }
                        }
                        else
                        {
                            // Pending cell - gray
                            cell.BackColor = Color.LightGray;
                        }
                    }
                }
            }
        }

        // Update position display
        private void UpdateTrayPosition()
        {
            int totalCells = m_trayRows * m_trayCols;
            int scannedCells = 0;
            
            if (m_trayData != null)
            {
                for (int r = 0; r < m_trayRows; r++)
                {
                    for (int c = 0; c < m_trayCols; c++)
                    {
                        if (!string.IsNullOrEmpty(m_trayData[r, c]))
                            scannedCells++;
                    }
                }
            }
            
            lblCurrentPos.Text = $"R{m_currentTrayRow}C{m_currentTrayCol} ({scannedCells}/{totalCells})";
            
            // Check if tray is complete
            if (m_currentTrayRow > m_trayRows)
            {
                lblCurrentPos.Text = $"COMPLETE! ({scannedCells}/{totalCells})";
                lblCurrentPos.ForeColor = Color.Green;
                btnSkipCell.Enabled = false;
                btnNextRow.Enabled = false;
                TgrBtn.Enabled = false;
                
                lblStatus.Text = $"Status: TRAY COMPLETE!\n{Path.GetFileName(m_excelFilePath)}\n\nAll {totalCells} cells scanned.\n\nCreate new file for next tray.";
            }
            
            UpdateGridHighlight();
        }

        // Move to next cell
        private void MoveToNextCell()
        {
            m_currentTrayCol++;
            if (m_currentTrayCol > m_trayCols)
            {
                m_currentTrayCol = 1;
                m_currentTrayRow++;
            }
            UpdateTrayPosition();
        }

        // Skip Cell button handler
        private void btnSkipCell_Click(object sender, EventArgs e)
        {
            if (m_currentTrayRow <= m_trayRows)
            {
                // Mark as skipped
                m_trayData[m_currentTrayRow - 1, m_currentTrayCol - 1] = "[SKIPPED]";
                
                // Save empty to Excel (or leave blank)
                SaveTrayCell("");
                
                lblStatus.Text = $"Status: Cell skipped\n{Path.GetFileName(m_excelFilePath)}\n\nSkipped: R{m_currentTrayRow}C{m_currentTrayCol}";
                
                MoveToNextCell();
            }
        }

        // Next Row button handler
        private void btnNextRow_Click(object sender, EventArgs e)
        {
            if (m_currentTrayRow <= m_trayRows)
            {
                // Skip remaining cells in current row
                while (m_currentTrayCol <= m_trayCols)
                {
                    m_trayData[m_currentTrayRow - 1, m_currentTrayCol - 1] = "[SKIPPED]";
                    SaveTrayCell("");
                    m_currentTrayCol++;
                }
                
                // Move to next row
                m_currentTrayCol = 1;
                m_currentTrayRow++;
                
                lblStatus.Text = $"Status: Moved to next row\n{Path.GetFileName(m_excelFilePath)}\n\nNow at: R{m_currentTrayRow}C{m_currentTrayCol}";
                
                UpdateTrayPosition();
            }
        }

        // Save data to tray Excel cell. Returns true on success, false on failure.
        private bool SaveTrayCell(string data)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(m_excelFilePath);
                using (var package = new ExcelPackage(fileInfo))
                {
                    var worksheet = package.Workbook.Worksheets[1];
                    // Row 1 is column header, so data starts at row 2
                    // Column 1 is row header, so data starts at column 2
                    worksheet.Cells[m_currentTrayRow + 1, m_currentTrayCol + 1].Value = data;
                    package.Save();
                }
                return true;
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Status: Save error!\n{ex.Message}";
                return false;
            }
        }

        // Auto-save for Tray Mode
        private void AutoSaveToTray(string receivedData)
        {
            if (string.IsNullOrEmpty(m_excelFilePath) || !File.Exists(m_excelFilePath))
                return;

            string rawData = receivedData?.Trim();
            if (string.IsNullOrWhiteSpace(rawData))
            {
                lblStatus.Text = $"Status: Empty scan\n{Path.GetFileName(m_excelFilePath)}\n\nNo data detected.\nPosition: R{m_currentTrayRow}C{m_currentTrayCol}";
                return;
            }

            // Check for duplicate
            if (m_scannedData.Contains(rawData))
            {
                m_duplicateCount++;
                lblStatus.Text = $"Status: DUPLICATE!\n{Path.GetFileName(m_excelFilePath)}\n\nData: {rawData}\n\nThis 2DID was already scanned!\nNot saved.\n\nDuplicates: {m_duplicateCount}";
                lblStatus.ForeColor = Color.Red;
                DataText.BackColor = Color.LightCoral;
                
                Timer resetTimer = new Timer();
                resetTimer.Interval = 1500;
                resetTimer.Tick += (s, args) =>
                {
                    DataText.BackColor = SystemColors.Control;
                    lblStatus.ForeColor = SystemColors.ControlText;
                    resetTimer.Stop();
                    resetTimer.Dispose();
                };
                resetTimer.Start();
                return;
            }

            // Check if tray is complete
            if (m_currentTrayRow > m_trayRows)
            {
                lblStatus.Text = "Status: Tray is complete!\n\nCreate new file for next tray.";
                return;
            }

            // Add to duplicate tracking BEFORE saving to Excel
            // This prevents the race where data is saved but UI hasn't updated,
            // causing the user to scan again and trigger a false duplicate
            m_scannedData.Add(rawData);
            m_trayData[m_currentTrayRow - 1, m_currentTrayCol - 1] = rawData;

            // Show immediate feedback that scan was accepted
            lblStatus.Text = $"Status: Saving...\n{Path.GetFileName(m_excelFilePath)}\n\nR{m_currentTrayRow}C{m_currentTrayCol}: {rawData}\n\nUnique scans: {m_scannedData.Count}";
            lblStatus.Refresh();
            UpdateGridHighlight();

            if (SaveTrayCell(rawData))
            {
                lblStatus.Text = $"Status: Saved!\n{Path.GetFileName(m_excelFilePath)}\n\nR{m_currentTrayRow}C{m_currentTrayCol}: {rawData}\n\nUnique scans: {m_scannedData.Count}";
                
                MoveToNextCell();
            }
            else
            {
                // Roll back tracking if save failed so user can retry
                m_scannedData.Remove(rawData);
                m_trayData[m_currentTrayRow - 1, m_currentTrayCol - 1] = null;
                UpdateGridHighlight();
            }
        }

        // Show User Guide
        private void btnHelp_Click(object sender, EventArgs e)
        {
            string userGuide = @"═══════════════════════════════════════════
       FIRSTSTEP APP - USER GUIDE
═══════════════════════════════════════════

SCAN MODES
───────────────────────────────────────────
• LOOSE UNIT: Sequential list scanning
• TRAY: Grid-based tray scanning

═══════════════════════════════════════════
         LOOSE UNIT MODE
═══════════════════════════════════════════

STEP 1: CREATE EXCEL FILE
   • Enter file name, click 'Create Excel File'
   • Choose save location

STEP 2: SCANNER AUTO-CONNECTS
   • Wait for 'Connected' status (green)

STEP 3: SCAN BARCODES
   • Click 'Trigger On' to scan
   • Data auto-saves to Excel
   • Duplicates are blocked

Excel Format: No. | Data | IP | Timestamp

═══════════════════════════════════════════
            TRAY MODE
═══════════════════════════════════════════

STEP 1: SET TRAY DIMENSIONS
   • Enter Rows and Columns count

STEP 2: CREATE EXCEL FILE
   • Creates grid matching tray layout

STEP 3: SCAN ROW BY ROW
   • Scan left-to-right, top-to-bottom
   • Current position shown in yellow
   • Green = scanned, Gray = pending
   • Red = skipped

TRAY CONTROLS:
   • Skip Cell: Skip empty position
   • Next Row: Jump to next row
   • Trigger On: Scan current cell

Excel Format: Pure grid (matches tray)

═══════════════════════════════════════════
              NOTES
═══════════════════════════════════════════
• Duplicates blocked in both modes
• Create new file to start fresh
• Close app properly to disconnect

═══════════════════════════════════════════";

            MessageBox.Show(userGuide, "User Guide - FirstStepApp", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
