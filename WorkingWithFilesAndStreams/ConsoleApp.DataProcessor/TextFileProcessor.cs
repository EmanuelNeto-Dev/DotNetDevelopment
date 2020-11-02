using System.IO;
using System.IO.Abstractions;

namespace ConsoleApp.DataProcessor
{
    public class TextFileProcessor
    {
        private readonly IFileSystem _fileSystem;

        public string InputFilePath { get; set; }
        public string OutputFilePath { get; set; }

        public TextFileProcessor(string inputFilePath, string outputFilePath)
            : this(inputFilePath, outputFilePath, new FileSystem()) { }

        public TextFileProcessor(string inputFilePath, string outputFilePath, IFileSystem fileSystem)
        {
            this.InputFilePath = inputFilePath;
            this.OutputFilePath = outputFilePath;
            this._fileSystem = fileSystem;
        }

        public void Process()
        {
            // * Using read all text * 
            //string originalText = File.ReadAllText(InputFilePath);
            //string processedText = originalText.ToUpperInvariant();
            //File.WriteAllText(OutputFilePath, processedText);

            // * Using read all lines *
            //string[] lines = File.ReadAllLines(InputFilePath, Encoding.UTF32);
            //lines[1] = lines[1].ToUpperInvariant();                             // We're assuming here that does exist more than 1 line in the file.
            //File.WriteAllLines(OutputFilePath, lines);

            // * Using Streams *
            #region Way 1
            //using (var inputFileStream = new FileStream(InputFilePath, FileMode.Open))
            //using (var inputStreamReader = new StreamReader(inputFileStream))
            //using (var outputFileStream = new FileStream(OutputFilePath, FileMode.Create))
            //using (var outputStreamWriter = new StreamWriter(outputFileStream))
            #endregion

            #region Way 2
            //using (var inputStreamReader = new StreamReader(InputFilePath))
            //using (var outputStreamWriter = new StreamWriter(OutputFilePath))
            //{
            //    var currentLineNumber = 1;
            //    while (!inputStreamReader.EndOfStream)
            //    {
            //        string line = inputStreamReader.ReadLine();
            //        string processedLine = line.ToUpperInvariant();
            //        bool isTheLastLine = inputStreamReader.EndOfStream;

            //        if (currentLineNumber == 2)
            //            Write(line.ToUpperInvariant());
            //        else
            //            Write(line);

            //        currentLineNumber++;

            //        void Write(string lineContent) 
            //        {
            //            if (isTheLastLine)
            //                outputStreamWriter.Write(lineContent);
            //            else
            //                outputStreamWriter.WriteLine(lineContent);
            //        }
            //    }
            //}   
            #endregion

            #region Way 3

            //using (FileStream output = File.Create(OutputFilePath))
            //{
            //    const int endOfStream = -1;
            //    int largestByte = 0;
            //    int currentByte = input.ReadByte();

            //    while (currentByte != endOfStream)
            //    {
            //        output.WriteByte((byte) currentByte);
            //        if (currentByte > largestByte)
            //            largestByte = currentByte;

            //        currentByte = input.ReadByte();
            //    }

            //    output.WriteByte((byte) largestByte);
            //}

            #endregion

            #region Way 4

            //using (FileStream inputFileStream = File.Open(InputFilePath, FileMode.Open, FileAccess.Read))
            //using (BinaryReader binaryStreamReader = new BinaryReader(inputFileStream))
            //using (FileStream outoutFileStream = File.Create(OutputFilePath))
            //using (BinaryWriter binaryStreamWriter = new BinaryWriter(outoutFileStream))
            //{
            //    byte largest = 0;

            //    while (binaryStreamReader.BaseStream.Position < binaryStreamReader.BaseStream.Length)
            //    {
            //        byte currentByte = binaryStreamReader.ReadByte();
            //        binaryStreamWriter.Write(currentByte);

            //        if (currentByte > largest)
            //            largest = currentByte;
            //    }

            //    binaryStreamWriter.Write(largest);
            //}

            #endregion

            #region Way 5 - Test Preparative

            using (var inputStreamReader = _fileSystem.File.OpenText(InputFilePath))
            using (var outputStreamWriter = _fileSystem.File.CreateText(OutputFilePath))
            {
                var currentLineNumber = 1;
                while (!inputStreamReader.EndOfStream)
                {
                    string line = inputStreamReader.ReadLine();
                    string processedLine = line.ToUpperInvariant();
                    bool isTheLastLine = inputStreamReader.EndOfStream;

                    if (currentLineNumber == 2)
                        Write(line.ToUpperInvariant());
                    else
                        Write(line);

                    currentLineNumber++;

                    void Write(string lineContent)
                    {
                        if (isTheLastLine)
                            outputStreamWriter.Write(lineContent);
                        else
                            outputStreamWriter.WriteLine(lineContent);
                    }
                }
            }

            #endregion

            #region Memory Stream

            //using (var memoryStream = new MemoryStream())
            //using (var memoryStreamWriter = new StreamWriter(memoryStream))
            //using (var fileStream = new FileStream(@"C:\data.txt", FileMode.Create))
            //{
            //    memoryStreamWriter.WriteLine("Line 1");
            //    memoryStreamWriter.WriteLine("Line 2");

            //    //Ensure everything written to memory stream
            //    memoryStreamWriter.Flush();

            //    memoryStream.WriteTo(fileStream);
            //}

            #endregion
        }
    }
}
