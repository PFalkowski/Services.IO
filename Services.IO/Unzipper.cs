using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.IO
{
    public class Unzipper : IUnzipper
    {
        public Dictionary<string, string> Unzip(IEnumerable<byte> zippedInput)
        {
            if (zippedInput == null) throw new ArgumentNullException(nameof(zippedInput));
            var bytes = zippedInput as byte[] ?? zippedInput.ToArray();
            if (bytes.Length == 0) throw new ArgumentException("Input must not be empty.", nameof(zippedInput));

            var buffer = new Dictionary<string, string>();
            using (var archive = new ZipArchive(new MemoryStream(bytes)))
            {
                foreach (var entry in archive.Entries)
                {
                    using (var reader = new StreamReader(entry.Open()))
                    {
                        buffer.Add(entry.FullName, reader.ReadToEnd());
                    }
                }
            }
            return buffer;
        }

        public async Task<Dictionary<string, string>> UnzipAsync(IEnumerable<byte> zippedInput, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (zippedInput == null) throw new ArgumentNullException(nameof(zippedInput));
            var bytes = zippedInput as byte[] ?? zippedInput.ToArray();
            if (bytes.Length == 0) throw new ArgumentException("Input must not be empty.", nameof(zippedInput));

            var buffer = new Dictionary<string, string>();
            using (var archive = new ZipArchive(new MemoryStream(bytes)))
            {
                foreach (var entry in archive.Entries)
                {
                    using (var reader = new StreamReader(entry.Open()))
                    {
                        buffer.Add(entry.FullName, await reader.ReadToEndAsync().ConfigureAwait(false));
                    }
                }
            }
            return buffer;
        }
    }
}
