using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Services.IO
{
    public class FileService : IFileService
    {
        public byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }

        public string[] ReadAllLines(string path)
        {
            return File.ReadAllLines(path);
        }

        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public void SaveToFile(string path, string contents)
        {
            File.WriteAllText(path, contents);
        }

        public async Task<byte[]> ReadAllBytesAsync(string path, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            byte[] result;
            using (var stream = File.Open(path, FileMode.Open))
            {
                result = new byte[stream.Length];
                await stream.ReadAsync(result, 0, (int)stream.Length, cancellationToken).ConfigureAwait(false);
            }
            return result;
        }

        public async Task<string[]> ReadAllLinesAsync(string path, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            var text = await ReadAllTextAsync(path, cancellationToken).ConfigureAwait(false);
            return text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }

        public async Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var reader = File.OpenText(path))
            {
                return await reader.ReadToEndAsync().ConfigureAwait(false);
            }
        }

        public async Task SaveToFileAsync(string path, string contents, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var sw = new StreamWriter(path))
            {
                await sw.WriteAsync(contents).ConfigureAwait(false);
            }
        }
    }
}
