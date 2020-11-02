using System;
using System.IO;
using System.Linq;
using System.IO.Abstractions;

namespace ConsoleApp.DataProcessor
{
    public class BinaryFileProcessor
    {
        private readonly IFileSystem _fileSystem;

        public string InputFilePath { get; set; }
        public string OutputFilePath { get; set; }

        public BinaryFileProcessor(string inputFilePath, string outputFilePath)
            : this(inputFilePath, outputFilePath, new FileSystem()) { }

        public BinaryFileProcessor(string inputFilePath, string outputFilePath, IFileSystem fileSystem)
        {
            this.InputFilePath = inputFilePath;
            this.OutputFilePath = outputFilePath;
            this._fileSystem = fileSystem;
        }

        public void Process()
        {
            #region Way 1

            //byte[] data = File.ReadAllBytes(InputFilePath);
            //byte largest = data.Max();
            //byte[] newData = new byte[data.Length + 1];
            //Array.Copy(data, newData, data.Length);
            //newData[newData.Length - 1] = largest;

            //File.WriteAllBytes(OutputFilePath, newData);

            #endregion

            #region Way 2 - Test Preparative

            using (Stream inputFileStream = _fileSystem.File.Open(InputFilePath, FileMode.Open, FileAccess.Read))
            using (BinaryReader binarytStreamReader = new BinaryReader(inputFileStream))
            using (Stream outputFileStream = _fileSystem.File.Create(OutputFilePath))
            using (BinaryWriter binaryStreamWriter = new BinaryWriter(outputFileStream))
            {
                int largest = 0;

                while (binarytStreamReader.BaseStream.Position < binarytStreamReader.BaseStream.Length)
                {
                    int currentByte = binarytStreamReader.ReadByte();
                    binaryStreamWriter.Write(currentByte);

                    if (currentByte > largest)
                    {
                        largest = currentByte;
                    }
                }

                binaryStreamWriter.Write(largest);
            }



            #endregion

        }
    }
}
