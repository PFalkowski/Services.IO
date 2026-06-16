# Services.IO

[![CI](https://github.com/PFalkowski/Services.IO/actions/workflows/ci.yml/badge.svg)](https://github.com/PFalkowski/Services.IO/actions/workflows/ci.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=PFalkowski_Services.IO&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=PFalkowski_Services.IO)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=PFalkowski_Services.IO&metric=coverage)](https://sonarcloud.io/summary/new_code?id=PFalkowski_Services.IO)
[![NuGet version](https://img.shields.io/nuget/v/Services.IO.svg)](https://www.nuget.org/packages/Services.IO/)
[![NuGet downloads](https://img.shields.io/nuget/dt/Services.IO.svg)](https://www.nuget.org/packages/Services.IO/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![Buy Me A Coffee](https://img.shields.io/badge/Buy%20Me%20A%20Coffee-support-yellow.svg)](https://www.buymeacoffee.com/piotrfalkowski)

Thin testable abstractions over common I/O operations — file read/write, directory enumeration, ZIP extraction, and HTTP download — each behind an interface so you can inject mocks in unit tests without hitting the real filesystem or network.

> Also check out [System.IO.Abstractions](https://github.com/System-IO-Abstractions/System.IO.Abstractions) for a broader abstraction layer over `System.IO`.

## Installation

```
dotnet add package Services.IO
```

## Quick start

```csharp
// Unzipping
var service = new Unzipper();
var result = await service.UnzipAsync(zippedBytes);
// result: Dictionary<string, string> — entry name → text content

// Downloading
var service = new Downloader();
var bytes = await service.GetBytesAsync(new Uri("https://example.com"));

// Reading a file
var service = new FileService();
var text = service.ReadAllText("myFile.txt");
var lines = await service.ReadAllLinesAsync("myFile.txt");

// Reading a directory
var service = new DirectoryService(new FileService());
var files = await service.ReadTopDirectoryAsync("./data", "*.csv");
// files: Dictionary<string, string> — file name → text content
```

## API

### `IUnzipper` / `Unzipper`

| Method | Description |
|--------|-------------|
| `Unzip(IEnumerable<byte>)` | Synchronously extract all entries to a `Dictionary<name, text>` |
| `UnzipAsync(IEnumerable<byte>, CancellationToken)` | Async extract |

### `IDownloader` / `Downloader`

| Method | Description |
|--------|-------------|
| `GetBytesAsync(Uri, CancellationToken)` | Download URL to `byte[]` using a shared `HttpClient` |

### `IFileService` / `FileService`

| Method | Description |
|--------|-------------|
| `ReadAllBytes(string)` | Read file as `byte[]` |
| `ReadAllBytesAsync(string, CancellationToken)` | Async read |
| `ReadAllLines(string)` | Read file as `string[]` |
| `ReadAllLinesAsync(string, CancellationToken)` | Async read |
| `ReadAllText(string)` | Read file as `string` |
| `ReadAllTextAsync(string, CancellationToken)` | Async read |
| `SaveToFile(string, string)` | Write text to file |
| `SaveToFileAsync(string, string, CancellationToken)` | Async write |

### `IDirectoryService` / `DirectoryService`

| Method | Description |
|--------|-------------|
| `ReadTopDirectory(string path, string pattern)` | Read all matching files in the top level of a directory → `Dictionary<name, text>` |
| `ReadTopDirectoryAsync(string, string, CancellationToken)` | Async read |

## License

MIT — see [LICENSE](LICENSE).
