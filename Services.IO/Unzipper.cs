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
            Validate(zippedInput, out var bytes);
            using (var archive = new ZipArchive(new MemoryStream(bytes)))
            {
                return ReadEntries(archive);
            }
        }

        public async Task<Dictionary<string, string>> UnzipAsync(IEnumerable<byte> zippedInput, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            Validate(zippedInput, out var bytes);
            return await Task.Run(() =>
            {
                using (var archive = new ZipArchive(new MemoryStream(bytes)))
                {
                    return ReadEntries(archive);
                }
            }, cancellationToken).ConfigureAwait(false);
        }

        private static void Validate(IEnumerable<byte> input, out byte[] bytes)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            bytes = input as byte[] ?? input.ToArray();
            if (bytes.Length == 0) throw new ArgumentException("Input must not be empty.", nameof(input));
        }

        private static Dictionary<string, string> ReadEntries(ZipArchive archive)
        {
            var result = new Dictionary<string, string>();
            foreach (var entry in archive.Entries)
            {
                using (var reader = new StreamReader(entry.Open()))
                {
                    result.Add(entry.FullName, reader.ReadToEnd());
                }
            }
            return result;
        }
    }
}
