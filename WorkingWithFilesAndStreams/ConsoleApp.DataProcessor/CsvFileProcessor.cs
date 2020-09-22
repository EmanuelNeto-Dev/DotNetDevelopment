using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper.Configuration;
using System.Linq;

namespace ConsoleApp.DataProcessor
{
    public class CsvFileProcessor
    {
        public string InputFilePath { get; set; }
        public string OutputFilePath { get; set; }

        public CsvFileProcessor(string inputFilePath, string outputFilePath)
        {
            InputFilePath = inputFilePath;
            OutputFilePath = outputFilePath;
        }

        public void Process()
        {
            using (StreamReader input = File.OpenText(InputFilePath))
            using (CsvReader csvReader = new CsvReader(input, CultureInfo.InvariantCulture))
            using (StreamWriter output = File.CreateText(OutputFilePath))
            using (var csvWriter = new CsvWriter(output, CultureInfo.InvariantCulture))
            {
                csvReader.Configuration.TrimOptions = TrimOptions.Trim;
                csvReader.Configuration.Comment = '@';              //The Default tag comment is '#'
                csvReader.Configuration.AllowComments = true;
                //csvReader.Configuration.IgnoreBlankLines = true;  //The Default is true
                //csvReader.Configuration.Delimiter = ";";
                //csvReader.Configuration.HasHeaderRecord = false;  //When we do not have any Record
                csvReader.Configuration.HeaderValidated = null;
                csvReader.Configuration.MissingFieldFound = null; //Used to ignored missing fields at the Header
                //csvReader.Configuration.RegisterClassMap<ProcessedOrderMap>();

                //IEnumerable<dynamic> records = csvReader.GetRecords<dynamic>(); //=> Reading dynamically
                //IEnumerable<Order> records = csvReader.GetRecords<Order>(); //=> Reading using the Order class
                IEnumerable<ProcessedOrder> records = csvReader.GetRecords<ProcessedOrder>();

                //csvWriter.WriteRecords(records); //=> Write all the records

                csvWriter.WriteHeader<ProcessedOrder>();
                csvWriter.NextRecord();

                var recordsArray = records.ToArray();
                for(int i = 0; i < recordsArray.Length; i++)
                {
                    csvWriter.WriteField(recordsArray[i].OrderNumber);
                    csvWriter.WriteField(recordsArray[i].Customer);
                    csvWriter.WriteField(recordsArray[i].Amount);

                    bool isLastRecord = i == recordsArray.Length - 1;
                    if (!isLastRecord)
                        csvWriter.NextRecord();
                }

                foreach (var record in records)
                {
                    Console.WriteLine(record.OrderNumber);          //When we do not have any Header, this field will be called Field1

                    #region Process using the Order class

                    //Console.WriteLine(record.CustomerNumber);
                    //Console.WriteLine(record.Description);
                    //Console.WriteLine(record.Quantity);

                    #endregion

                    #region Process using the ProcessedOrder class

                    Console.WriteLine(record.Customer);
                    Console.WriteLine(record.Amount);

                    #endregion
                }
            }
        }
    }
}
