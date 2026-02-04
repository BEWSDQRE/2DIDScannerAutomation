using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Keyence.AutoID.SDK;
using ClosedXML.Excel;

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

        // Hardcoded IP addresses for auto-connect
        private const string TARGET_NIC_IP = "192.168.100.11";
        private const string TARGET_READER_IP = "192.168.100.100";
        private bool m_autoConnecting = false;

        public Form1()
        {
            InitializeComponent();
            m_nicList = m_searcher.ListUpNic();
            if (m_nicList != null)
            {
                for (int i = 0; i < m_nicList.Count; i++)
                {
                    NICcomboBox.Items.Add(m_nicList[i].NicIpAddr);
                }
            }
            NICcomboBox.SelectedIndex = 0;

            // Auto-connect on form load
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
                //ExecCommand("command")is for sending a command and getting a command response.
                ReceivedDataWrite(m_reader.ExecCommand("LON"));
            }
        }
        //delegateUserControl is for controlling UserControl from other threads.
        private delegate void delegateUserControl(string str);
        private void ReceivedDataWrite(string receivedData)
        {
            DataText.Text=("[" + m_reader.IpAddress + "][" + DateTime.Now + "]" + receivedData);
            
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

                    try
                    {
                        // Create a new Excel workbook with headers
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("ScanData");
                            
                            // Create header row with column titles
                            worksheet.Cell(1, 1).Value = "No.";
                            worksheet.Cell(1, 2).Value = "Scan Data";
                            worksheet.Cell(1, 3).Value = "IP Address";
                            worksheet.Cell(1, 4).Value = "Timestamp";
                            
                            // Style headers
                            var headerRange = worksheet.Range("A1:D1");
                            headerRange.Style.Font.Bold = true;
                            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                            
                            // Set column widths
                            worksheet.Column(1).Width = 6;   // No.
                            worksheet.Column(2).Width = 35;  // Scan Data
                            worksheet.Column(3).Width = 18;  // IP Address
                            worksheet.Column(4).Width = 22;  // Timestamp
                            
                            workbook.SaveAs(m_excelFilePath);
                        }

                        m_currentRow = 2; // Start data from row 2 (row 1 is header)
                        lblStatus.Text = $"Status: File created\n{Path.GetFileName(m_excelFilePath)}\n\nAuto-save enabled!\nScans will be saved automatically.\nNext row: {m_currentRow}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error creating file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Auto-save scan data to Excel
        private void AutoSaveToExcel(string receivedData)
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

            try
            {
                using (var workbook = new XLWorkbook(m_excelFilePath))
                {
                    var worksheet = workbook.Worksheet(1);
                    
                    // Save data in separate columns
                    int rowNumber = m_currentRow - 1; // Row number (1, 2, 3...)
                    worksheet.Cell(m_currentRow, 1).Value = rowNumber;           // No.
                    worksheet.Cell(m_currentRow, 2).Value = rawData;             // Scan Data only
                    worksheet.Cell(m_currentRow, 3).Value = m_reader.IpAddress;  // IP Address
                    worksheet.Cell(m_currentRow, 4).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Timestamp
                    
                    workbook.Save();
                }

                lblStatus.Text = $"Status: Auto-saved!\n{Path.GetFileName(m_excelFilePath)}\n\nRow: {m_currentRow}\nData: {rawData}\n\nNext row: {m_currentRow + 1}";
                m_currentRow++;
            }
            catch (Exception ex)
            {
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

        // Show User Guide
        private void btnHelp_Click(object sender, EventArgs e)
        {
            string userGuide = @"═══════════════════════════════════════════
       FIRSTSTEP APP - USER GUIDE
═══════════════════════════════════════════

STEP 1: CREATE EXCEL FILE
───────────────────────────────────────────
   • Enter a file name in 'File Name' textbox
   • Click 'Create Excel File'
   • Choose save location in the dialog
   • Status shows 'File created'

STEP 2: SCANNER AUTO-CONNECTS
───────────────────────────────────────────
   • Scanner connects automatically on startup
   • Wait for 'Connected' status (green text)
   • LiveView camera feed will start
   • No manual selection needed!

STEP 3: SCAN BARCODES
───────────────────────────────────────────
   • Click 'Trigger On' to scan
   • Data is AUTO-SAVED to Excel instantly!
   • Empty scans are automatically skipped
   • Status shows saved data and row number

Excel Output Format:
   Column A: Row Number (1, 2, 3...)
   Column B: Scan Data (barcode only)
   Column C: IP Address
   Column D: Timestamp


NOTES
───────────────────────────────────────────
• Scanner auto-connects to 192.168.100.100
• Data is saved AUTOMATICALLY on each scan
• Empty/null scans are skipped (not saved)
• Create Excel file BEFORE scanning
• Create a new file to start fresh
• Close the app properly to disconnect

═══════════════════════════════════════════";

            MessageBox.Show(userGuide, "User Guide - FirstStepApp", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
