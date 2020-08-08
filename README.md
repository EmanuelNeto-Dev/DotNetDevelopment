# DotNetDevelopment
Repositório para armazenamento de projetos em .Net e C#

 * 1. Managing files and directories
 * 2. Monitoring the file system for changes
    - Infroducing the FileSystemWatcher class
       1. Buffer size
       2. Configuring notification filters
       3. Additional properties 
     - Refactoring the application to use FileSystemWatchers
     - Ignoring duplicates in FileSystemWatcher events
       1. ConcurrentDictionary
       2. MemoryCache
     - Add Existing file processing 
     - Testing considerations

    FileSystemWatcher Testing Considerations:
     - Basig file Operations (copy, move, delete, overwrite, rename)
     - Incremental write of large file(s)
     - Saving from different applications
     - High volume of changes (buffer)
     - Files only processed once
     - Network/mapped drive recovery => Error event
     - Existing file processing
