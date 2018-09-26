using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.IO
{
    public interface IFileService
    {
        Task SaveToFileAsync(string path, string contents, CancellationToken cancellationToken);
        Task<byte[]> ReadAllBytesAsync(string path, CancellationToken cancellationToken);
        Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken);
        Task<string[]> ReadAllLinesAsync(string path, CancellationToken cancellationToken);
        void SaveToFile(string path, string contents);
        byte[] ReadAllBytes(string path);
        string ReadAllText(string path);
        string[] ReadAllLines(string path);
    }
}
