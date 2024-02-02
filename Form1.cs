
using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Collections.Generic;

namespace C__Stock_Project
{
    /// <summary>
    /// Main form for the stock charting application.
    /// This form displays candlestick and volume charts for stock data and allows for pattern highlighting.
    /// </summary>
    public partial class Form1 : Form
    {
        // Private fields to store the candlesticks data, start date, end date, and file name
        private BindingList<SmartCandlestick> allCandlesticks;
        private DateTime startDate;
        private DateTime endDate;
        private string fileName;

        /// <summary>
        /// Constructs a new instance of the Form1 class.
        /// </summary>
        /// <param name="allCandlesticks">The list of candlesticks to be displayed.</param>
        /// <param name="startDate">The start date for the chart.</param>
        /// <param name="endDate">The end date for the chart.</param>
        /// <param name="fileName">The name of the file containing stock data.</param>


        public Form1(BindingList<SmartCandlestick> allCandlesticks, DateTime startDate, DateTime endDate, string fileName)
        {
            // Initialize the form components and set private fields with provided values
            InitializeComponent();
            this.allCandlesticks = allCandlesticks;
            this.startDate = startDate;
            this.endDate = endDate;
            this.fileName = fileName;

            // Create a filtered list of candlesticks based on the specified date range
            var filteredData = new BindingList<SmartCandlestick>();

            foreach (var candle in allCandlesticks)
            {
                if (candle.Date >= startDate && candle.Date <= endDate)
                {
                    filteredData.Add(candle);
                }
            }

            // Populate the pattern combo box
            PopulatePatternComboBox();

            // Configure and display the candlestick chart with the filtered data
            ConfigureCandlestickChart(filteredData);

            // Configure and display the volume chart with the filtered data
            ConfigureVolumeChart(filteredData);

            // Set the form's title to the name of the file (excluding extension)
            this.Text = Path.GetFileNameWithoutExtension(fileName);

            // Set the date picker values to the specified start and end dates
            dateTimePicker_start.Value = startDate;
            dateTimePicker_end.Value = endDate;
        }


        /// <summary>
        /// Populates the combo box with available pattern recognizers.
        /// </summary>
        private void PopulatePatternComboBox()
        {
            // Clear existing items in the combo box
            comboBox_patterns.Items.Clear();

            // Gather types of pattern recognizers from the executing assembly
            var recognizerTypes = new List<Type>();
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                // Check if the type is a subclass of PatternRecognizer and not abstract
                if (type.IsSubclassOf(typeof(PatternRecognizer)) && !type.IsAbstract)
                {
                    recognizerTypes.Add(type);
                }
            }

            // Create an instance of each recognizer type
            foreach (Type type in recognizerTypes)
            {
                // Create an instance of the recognizer type
                var recognizer = Activator.CreateInstance(type) as PatternRecognizer;

                // Check if the instance was successfully created and add it to the combo box
                if (recognizer != null)
                {
                    comboBox_patterns.Items.Add(recognizer);
                }
            }

            // Set the display member to show the friendly name in the combo box
            comboBox_patterns.DisplayMember = "PatternName";
        }


        /// <summary>
        /// Modifies the chart data according to the user-selected date range and pattern.
        /// </summary>
        /// <param name="sender">The origin of the event.</param>
        /// <param name="e">The event information.</param>

        private void button_update_Click(object sender, EventArgs e)
        {
            // Update the startDate and endDate based on the selected values from the date pickers
            startDate = dateTimePicker_start.Value;
            endDate = dateTimePicker_end.Value;

            // Ensure the validity of the selected date range
            if (startDate <= endDate)
            {
                // Manually filter the candlesticks list for the specified date range
                var filteredData = new BindingList<SmartCandlestick>();
                foreach (var candle in allCandlesticks)
                {
                    // Include candles falling within the selected date range
                    if (candle.Date >= startDate && candle.Date <= endDate)
                    {
                        filteredData.Add(candle);
                    }
                }

                // Update the charts with the filtered data
                ConfigureCandlestickChart(filteredData);
                ConfigureVolumeChart(filteredData);

                // Retrieve the selected pattern from the combo box
                var selectedPattern = comboBox_patterns.SelectedItem as PatternRecognizer;

                // Check if a valid pattern is selected
                if (selectedPattern == null)
                {
                    // Handle the case where no pattern is selected or the chosen pattern is not a PatternRecognizer
                    return; // Exit the method or implement custom handling as necessary
                }

                // Highlight the selected pattern in the filtered data
                HighlightPattern(filteredData, selectedPattern);
            }
            else
            {
                // Display an error message if the selected start date is after the end date
                MessageBox.Show("The start date must be before the end date.", "Date Range Error");
            }
        }


        /// <summary>
        /// Configures and populates the candlestick chart with the provided data.
        /// </summary>
        /// <param name="candlestickData">The data to be displayed on the candlestick chart.</param>
        private void ConfigureCandlestickChart(BindingList<SmartCandlestick> candlestickData)
        {
            // Configure the ChartArea
            chart1.ChartAreas.Clear();
            var chartArea = new ChartArea("CandlestickArea");
            chart1.ChartAreas.Add(chartArea);

            // Add and configure the Series
            chart1.Series.Clear();
            var series = new Series("CandlestickSeries")
            {
                ChartArea = "CandlestickArea",
                ChartType = SeriesChartType.Candlestick,
                XValueType = ChartValueType.DateTime,
                CustomProperties = "PriceDownColor=Red,PriceUpColor=Green"
            };
            chart1.Legends.Clear();
            chart1.Series.Add(series);

            // Bind the series to the data
            series.XValueMember = "Date";
            series.YValueMembers = "High,Low,Open,Close";
            chart1.DataSource = candlestickData;
            chart1.DataBind();

            // Configure the X Axis
            chartArea.AxisX.LabelStyle.Format = "MMM dd yyyy";
            chartArea.AxisX.MajorGrid.Enabled = false;

            // Configure the Y Axis
            chartArea.AxisY.MajorGrid.Enabled = false;
        }



        /// <summary>
        /// Configures and populates the volume chart with the provided data.
        /// </summary>
        /// <param name="candlestickData">The data to be displayed on the volume chart.</param>
        private void ConfigureVolumeChart(BindingList<SmartCandlestick> candlestickData)
        {
            // Configure the ChartArea
            chart2.ChartAreas.Clear();
            var chartArea = new ChartArea("VolumeArea");
            chart2.ChartAreas.Add(chartArea);

            // Add and configure the Series
            chart2.Series.Clear();
            var series = new Series("VolumeSeries")
            {
                ChartArea = "VolumeArea",
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.DateTime,
                CustomProperties = "PointWidth=0.6" // Adjust the width of the bars
            };
            chart2.Legends.Clear();
            chart2.Series.Add(series);

            // Bind the series to the data
            series.XValueMember = "Date";
            series.YValueMembers = "Volume";
            chart2.DataSource = candlestickData;
            chart2.DataBind();

            // Configure the X Axis
            chartArea.AxisX.LabelStyle.Format = "MMM dd yyyy";
            chartArea.AxisX.MajorGrid.Enabled = false;

            // Configure the Y Axis
            chartArea.AxisY.Title = "Volume";
            chartArea.AxisY.MajorGrid.Enabled = false;
        }


        /// <summary>
        /// Computes the minimum interval between candlesticks on the X-axis of the chart.
        /// </summary>
        /// <param name="chart">The chart for which the interval is calculated.</param>
        /// <param name="seriesName">The name of the series in the chart.</param>
        /// <returns>The minimum interval between candlesticks.</returns>
        private double CalculateMinInterval(Chart chart, string seriesName)
        {
            double minInterval = double.MaxValue;
            DataPoint previousPoint = null;

            foreach (DataPoint currentPoint in chart.Series[seriesName].Points)
            {
                if (previousPoint != null)
                {
                    double interval = currentPoint.XValue - previousPoint.XValue;
                    if (interval < minInterval)
                    {
                        minInterval = interval;
                    }
                }
                previousPoint = currentPoint;
            }

            return minInterval;
        }

        /// <summary>
        /// Highlights the specified candlestick pattern on the chart.
        /// </summary>
        /// <param name="candlesticks">The list of candlesticks to be considered for highlighting.</param>
        /// <param name="patternRecognizer">The recognizer for the candlestick pattern.</param>
        private void HighlightPattern(BindingList<SmartCandlestick> candlesticks, PatternRecognizer patternRecognizer)
        {
            // Clear any previous annotations
            chart1.Annotations.Clear();

            // Calculate the minimum interval between candlesticks on the X-axis
            double minInterval = CalculateMinInterval(chart1, "CandlestickSeries");
            double annotationWidth = minInterval * 0.8; // Use 80% of the minimum interval for the annotation width

            for (int i = 0; i < candlesticks.Count; i++)
            {
                if (patternRecognizer.IsPatternPresent(candlesticks, i))
                {
                    var (start, end) = patternRecognizer.GetPatternRange(candlesticks, i);
                    for (int j = start; j <= end; j++)
                    {
                        var candle = candlesticks[j];
                        RectangleAnnotation annotation = new RectangleAnnotation
                        {
                            AxisX = chart1.ChartAreas["CandlestickArea"].AxisX,
                            AxisY = chart1.ChartAreas["CandlestickArea"].AxisY,
                            AnchorX = candle.Date.ToOADate(),
                            AnchorY = candle.Low,
                            IsSizeAlwaysRelative = false,
                            Width = annotationWidth, // Utilize the dynamic width
                            Height = candle.Low - candle.High,
                            LineColor = Color.Blue,
                            LineWidth = 2,
                            BackColor = Color.FromArgb(50, Color.Blue) // Semi-transparent blue
                        };
                        chart1.Annotations.Add(annotation);
                    }
                }
            }
        }

    }
}
