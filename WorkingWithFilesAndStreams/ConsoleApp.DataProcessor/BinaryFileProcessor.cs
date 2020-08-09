using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.DataProcessor
{
    public class BinaryFileProcessor
    {
        public string InputFilePath { get; set; }
        public string OutputFilePath { get; set; }

        public BinaryFileProcessor(string inputFilePath, string outputFilePath)
        {
            this.InputFilePath = inputFilePath;
            this.OutputFilePath = outputFilePath;
        }

        public void Process()
        {
            byte[] data = File.ReadAllBytes(InputFilePath);
            byte largest = data.Max();
            byte[] newData = new byte[data.Length + 1];
            Array.Copy(data, newData, data.Length);
            newData[newData.Length - 1] = largest;

            File.WriteAllBytes(OutputFilePath, newData);
        }
    }
}
