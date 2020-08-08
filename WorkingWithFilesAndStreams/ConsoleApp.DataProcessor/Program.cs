using System;
using System.Collections.Concurrent;
using System.IO;
using System.Runtime.Caching;
using System.Threading;
using static System.Console;

namespace ConsoleApp.DataProcessor
{
    class Program
    {
        /* [01] - It's just an implemantation of an alternative to ignore Duplicates during de process
        --
        private static ConcurrentDictionary<string, string> FilesToProcess =
            new ConcurrentDictionary<string, string>();
        --
        */

        private static MemoryCache FilesToProcess = MemoryCache.Default;

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

                ProcessExistingFiles(directoryToWatch);

                using(var inputFileWatcher = new FileSystemWatcher(directoryToWatch))
                //[01] - using(var time = new Timer(ProcessFiles, null, 0, 1000))
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

        private static void ProcessExistingFiles(string directoryToWatch)
        {
            WriteLine($"Checking {directoryToWatch} for existing files.");
            foreach (var filePath in Directory.EnumerateFiles(directoryToWatch))
            {
                WriteLine($" - Found {filePath}");
                AddToCache(filePath);
            }
        }

        private static void FileCreated(object sender, FileSystemEventArgs e)
        {
            WriteLine($"* File created: {e.Name} - Type: {e.ChangeType}.");

            //var fileProcessor = new FileProcessor(e.FullPath);
            //fileProcessor.Process();

            //[01] - FilesToProcess.TryAdd(e.FullPath, e.FullPath);
            AddToCache(e.FullPath);
        }

        private static void FileChanged(object sender, FileSystemEventArgs e)
        {
            WriteLine($"* File changed: {e.Name} - Type: {e.ChangeType}.");

            //var fileProcessor = new FileProcessor(e.FullPath);
            //fileProcessor.Process();

            //[01] - FilesToProcess.TryAdd(e.FullPath, e.FullPath);
            AddToCache(e.FullPath);
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

        private static void AddToCache(string fullPath)
        {
            var item = new CacheItem(fullPath, fullPath);
            var policy = new CacheItemPolicy
            {
                RemovedCallback = ProcessFile,
                SlidingExpiration = TimeSpan.FromSeconds(2),    // If the item stored in cache, doesn't have accessed in the last 2 sec. It will be removed from the cache
            };

            FilesToProcess.Add(item, policy);
        }

        /* [01] */
        //private static void ProcessFiles(Object stateInfo)
        //{
        //    foreach (var fileName in FilesToProcess.Keys)
        //    {
        //        if (FilesToProcess.TryRemove(fileName, out _))
        //        {
        //            var fileProcessor = new FileProcessor(fileName);
        //            fileProcessor.Process();
        //        }
        //    }
        //}

        private static void ProcessFile(CacheEntryRemovedArguments args)
        {
            WriteLine($"* Cache Item removed: {args.CacheItem.Key} because {args.RemovedReason}.");

            if (args.RemovedReason == CacheEntryRemovedReason.Expired)
            {
                var fileProcessor = new FileProcessor(args.CacheItem.Key);
                fileProcessor.Process();
            }
            else
            {
                WriteLine($"WARNING: {args.CacheItem.Key} was removed unexpectedly and may not process.");
            }
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
