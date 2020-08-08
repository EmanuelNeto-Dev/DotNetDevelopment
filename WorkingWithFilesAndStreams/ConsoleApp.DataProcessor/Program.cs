using System;
using System.IO;
using static System.Console;

namespace ConsoleApp.DataProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Parsing command line options");

            // Commmand line validation omitted for brevity

            var directoryToWatch = args[0];

            if (!Directory.Exists(directoryToWatch))
            {
                WriteLine($"ERROR: {directoryToWatch} does not exist.");
            }
            else
            {
                WriteLine($"Watching directory {directoryToWatch} for changes.");

                using(var inputFileWatcher = new FileSystemWatcher(directoryToWatch))
                {
                    inputFileWatcher.IncludeSubdirectories = false;             //If don't want to monitor the subDirectories
                    inputFileWatcher.InternalBufferSize = 32768;                // 32 KB -> It's just necessary if we would have a big amount of changes in short period of time
                    inputFileWatcher.Filter = "*.*";                            // To monitor all types of files that we have
                    inputFileWatcher.NotifyFilter = NotifyFilters.FileName;     // it depends so much of the type of application that we are building    

                    inputFileWatcher.Created += FileCreated;
                    inputFileWatcher.Changed += FileChanged;
                    inputFileWatcher.Deleted += FileDeleted;
                    inputFileWatcher.Renamed += FileRenamed;
                    inputFileWatcher.Error += WatcherError;

                    inputFileWatcher.EnableRaisingEvents = true;                // By default the FileSystemWatcher doesn't raise any event. It's necessary to set true

                    WriteLine("Press enter to quit");
                    ReadLine();
                }
            }
        }

        private static void FileCreated(object sender, FileSystemEventArgs e)
        {
            WriteLine($"* File created: {e.Name} - Type: {e.ChangeType}.");
        }

        private static void FileChanged(object sender, FileSystemEventArgs e)
        {
            WriteLine($"* File changed: {e.Name} - Type: {e.ChangeType}.");
        }

        private static void FileDeleted(object sender, FileSystemEventArgs e)
        {
            WriteLine($"* File deleted: {e.Name} - Type: {e.ChangeType}.");
        }

        private static void FileRenamed(object sender, RenamedEventArgs e)
        {
            WriteLine($"* File renamed: {e.OldName} to {e.Name} - Type: {e.ChangeType}.");
        }

        private static void WatcherError(object sender, ErrorEventArgs e)
        {
            WriteLine($"ERROR: file system watching may no longer be active: {e.GetException()}.");
        }

        private static void ProcessSingleFile(string filePath)
        {
            var fileProcessor = new FileProcessor(filePath);
            fileProcessor.Process();
        }

        private static void ProcessDirectory(string directoryPath, string fileType)
        {
            //To get all files
            //var allFiles = Directory.GetFiles(directoryPath); 

            switch (fileType)
            {
                case "TEXT":
                    string[] textFiles = Directory.GetFiles(directoryPath, "*.txt");
                    foreach (var textFilePath in textFiles)
                    {
                        var fileProcessor = new FileProcessor(textFilePath);
                        fileProcessor.Process();
                    }
                    break;
                default:
                    WriteLine($"ERROR: {fileType} is not supported.");
                    break;
            }
        }
    }
}
