
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace C__Stock_Project
{
    /// <summary>
    /// The main form of the application, responsible for loading stock data and displaying individual chart forms.
    /// </summary>
    public partial class FormMain : Form
    {
        private CsvReaderService _csvReaderService = new CsvReaderService();

        /// <summary>
        /// Initializes a new instance of the FormMain class.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            dateTimePicker_start.Value = new DateTime(2022, 11, 10);
        }
        /// <summary>
        /// Event handler for the 'Load' button click.
        /// Opens a dialog to select CSV files and then displays each file in a new chart form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void button_load_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Path.GetFullPath(@"..\..\..\Stock Data"),
                Filter = "Daily Files|*-Day.csv|Weekly Files|*-Week.csv|Monthly Files|*-Month.csv|All Files (*.csv)|*.csv",
                FilterIndex = 4,  // Set to 4 to show all files by default
                Title = "Select a CSV File",
                Multiselect = true
            };

            // If the user selects a file and clicks "Open"...
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string selectedFilePath in openFileDialog.FileNames)
                {
                  
                    // Use CsvReaderService to read the data.
                    BindingList<SmartCandlestick> allCandlestickData = _csvReaderService.ReadCsvData(selectedFilePath);

                    // Get the selected date range.
                    DateTime startDate = dateTimePicker_start.Value.Date;
                    DateTime endDate = dateTimePicker_end.Value.Date;

                    // Validate the date range.
                    if (startDate > endDate)
                    {
                        MessageBox.Show("End date must be after start date.", "Invalid Date Range",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Form1 chartFrom = new Form1(allCandlestickData, startDate, endDate, selectedFilePath);
                    chartFrom.Show();
                }
            }
        }
    }
}
