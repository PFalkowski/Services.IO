# Services.IO

[![NuGet version (Services.IO)](https://img.shields.io/nuget/v/Services.IO.svg)](https://www.nuget.org/packages/Services.IO/)
[![Licence (Services.IO)](https://img.shields.io/github/license/mashape/apistatus.svg)](https://choosealicense.com/licenses/mit/)

Handy-dandy unbloater for ease of testability of other parts of your system, that normally would be bound to system calls. Also check out https://github.com/System-IO-Abstractions/System.IO.Abstractions for abstraction over System calls in .NET Framework.

```c#
        // Unzipping
        
        byte[] Input => new byte[] { 80, 75, 3, 4, 20, 0, 0, 0, 0, 0, 249,
                96, 107, 71, 32, 124, 203, 240, 12, 0, 0, 0, 12, 0, 0, 0, 11, 0,
                0, 0, 90, 105, 112, 84, 101, 115, 116, 46, 116, 120, 116, 84, 101,
                115, 116, 90, 105, 112, 112, 70, 105, 108, 101, 80, 75, 1, 2, 20,
                0, 20, 0, 0, 0, 0, 0, 249, 96, 107, 71, 32, 124, 203, 240, 12, 0,
                0, 0, 12, 0, 0, 0, 11, 0, 0, 0, 0, 0, 0, 0, 1, 0, 32, 0, 0, 0, 0,
                0, 0, 0, 90, 105, 112, 84, 101, 115, 116, 46, 116, 120, 116, 80,
                75, 5, 6, 0, 0, 0, 0, 1, 0, 1, 0, 57, 0, 0, 0, 53, 0, 0, 0, 0, 0 };

        string ExpectedFileContent = "TestZippFile";


        var service = new Unzipper();
        var result = await service.UnzipAsync(Input);
```        

```c#
        // Downloading
        
        const string example = "http://example.com";
        var service = new Downloader();
        var result = await service.GetBytesAsync(new Uri(example));
```    

```c#
        // Reading file
        
        var file = "myFile.txt";
        var service = new FileService();
        var contents = service.ReadAllText(file.FullName);            
```   

```c#
        // Reading directory to dictionary
        
        var pattern = "*.txt";
        var service = new DirectoryService(new FileService());
        var actual = await service.ReadTopDirectoryAsync(file1.Directory.FullName, pattern);
            
```   
