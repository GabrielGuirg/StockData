using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Linq;
using System.ComponentModel;

namespace C__Stock_Project
{
    // Class: Responsible for extracting candlestick data from a CSV file.
    public class CsvReaderService
    {
        // Method: Extracts candlestick data from the specified CSV file path and returns it as a BindingList of SmartCandlestick objects.
        public BindingList<SmartCandlestick> ReadCsvData(string filePath)
        {
            // Ensure the file exists before attempting to open it.
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found", filePath);

            // Open the file and retrieve the data.
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                // Obtain SmartCandlestick records from the CSV file and present them as a BindingList.
                return new BindingList<SmartCandlestick>(csv.GetRecords<SmartCandlestick>().ToList());
            }
        }
    }
}

