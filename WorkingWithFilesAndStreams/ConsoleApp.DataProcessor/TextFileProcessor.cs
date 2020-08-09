using System.IO;
using System.Text;

namespace ConsoleApp.DataProcessor
{
    public class TextFileProcessor
    {
        public string InputFilePath { get; set; }
        public string OutputFilePath { get; set; }

        public TextFileProcessor(string inputFilePath, string outputFilePath)
        {
            this.InputFilePath = inputFilePath;
            this.OutputFilePath = outputFilePath;
        }

        public void Process()
        {
            //Using read all text
            //string originalText = File.ReadAllText(InputFilePath);
            //string processedText = originalText.ToUpperInvariant();
            //File.WriteAllText(OutputFilePath, processedText);

            //Using read all lines
            string[] lines = File.ReadAllLines(InputFilePath, Encoding.UTF32);
            lines[1] = lines[1].ToUpperInvariant();                             // We're assuming here that does exist more than 1 line in the file.
            File.WriteAllLines(OutputFilePath, lines);
        }
    }
}
