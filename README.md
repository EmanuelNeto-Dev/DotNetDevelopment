# DotNetDevelopment
Repositório para armazenamento de projetos em .Net e C#

 * Managing files and directories
 * Monitoring the file system for changes
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
  * Reading and Writing entire files into the memory
    - Files reading line by line & entirely
    - Data reading in bytes

# FileSystemWatcher Testing Considerations:
    - Basig file Operations (copy, move, delete, overwrite, rename)
    - Incremental write of large file(s)
    - Saving from different applications
    - High volume of changes (buffer)
    - Files only processed once
    - Network/mapped drive recovery => Error event
    - Existing file processing
    
# Reading and Writing files into Memory Considerations:
    - Benefits:
        1. Simple code
        2. Easier to write
        3. Easy do read & maintain
    - Drawbacks:
        1. May be Slow
        2. Can bring the program to crach (Out of Memory problems)
        3. There is no random access / seeking
