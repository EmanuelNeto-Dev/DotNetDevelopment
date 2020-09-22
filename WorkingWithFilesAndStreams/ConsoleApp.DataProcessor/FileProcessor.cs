using System;
using System.IO;
using static System.Console;

namespace ConsoleApp.DataProcessor
{
    internal class FileProcessor
    {
        private static readonly string BackupDirectoryName = "backup";
        private static readonly string InProgressDirectoryName = "processing";
        private static readonly string CompletedDirectoryName = "complete";

        public string InputFilePath { get; }

        public FileProcessor(string filePath)
        {
            InputFilePath = filePath;
        }

        public void Process()
        {
            WriteLine($"Begin process of {InputFilePath}.");

            //Check if file exists
            if(!File.Exists(InputFilePath))
            {
                WriteLine($"ERROR: file {InputFilePath} does not exist.");
                return;
            }

            string rootDirectoryPath = new DirectoryInfo(InputFilePath).Parent.Parent.FullName;
            WriteLine($"Rood data path is {rootDirectoryPath}.");

            string inputFileDirectoryPath = Path.GetDirectoryName(InputFilePath);
            string backupDirectoryPath = Path.Combine(rootDirectoryPath, BackupDirectoryName);
            
            //Verifying if directory already exists
            if(!Directory.Exists(backupDirectoryPath))
            {
                WriteLine($"Creating {backupDirectoryPath}");
                Directory.CreateDirectory(backupDirectoryPath);
            }

            //Copy file to backup dir
            string inputFileName = Path.GetFileName(InputFilePath);
            string backupFilePath = Path.Combine(backupDirectoryPath, inputFileName);
            WriteLine($"Copying {InputFilePath} to {backupFilePath}.");
            File.Copy(InputFilePath, backupFilePath, true);

            //Move to in progress dir
            if(!Directory.Exists(Path.Combine(rootDirectoryPath, InProgressDirectoryName)))
            {
                Directory.CreateDirectory(Path.Combine(rootDirectoryPath, InProgressDirectoryName));
            }

            string inProgressFilePath =
                Path.Combine(rootDirectoryPath, InProgressDirectoryName, inputFileName);

            if(File.Exists(inProgressFilePath))
            {
                WriteLine($"ERROR: a file with the name {inProgressFilePath} is already being processed");
                return;
            }

            WriteLine($"Moving {InputFilePath} to {inProgressFilePath}.");
            File.Move(InputFilePath, inProgressFilePath);

            //Determine the file type
            string extension = Path.GetExtension(InputFilePath);

            string completedDirectoryPath = Path.Combine(rootDirectoryPath, CompletedDirectoryName);
            if (!Directory.Exists(Path.Combine(rootDirectoryPath, CompletedDirectoryName)))
            {
                Directory.CreateDirectory(Path.Combine(rootDirectoryPath, CompletedDirectoryName));
            }

            var completedFileName =
                $"{Path.GetFileNameWithoutExtension(InputFilePath)}-{Guid.NewGuid()}{extension}";

            string completedFilePath = Path.Combine(completedDirectoryPath, completedFileName);

            switch (extension)
            {
                case ".txt":
                    var textProcessor = new TextFileProcessor(inProgressFilePath, completedFilePath);
                    textProcessor.Process();
                    break;
                case ".data":
                    var binaryProcessor = new BinaryFileProcessor(inProgressFilePath, completedFilePath);
                    binaryProcessor.Process();
                    break;
                case ".csv":
                    var csvProcessor = new CsvFileProcessor(inProgressFilePath, completedFilePath);
                    csvProcessor.Process();
                    break;
                default:
                    WriteLine($"The extension {extension} is an unsupported type of file.");
                    break;
            }

            WriteLine($"Completed processing of {inProgressFilePath}.");
            WriteLine($"Deleting {inProgressFilePath}.");

            File.Delete(inProgressFilePath);
        }

        
    }
}