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
        // TODO: add to interface, unit test
        public Dictionary<string, string> Unzip(IEnumerable<byte> zippedInput)
        {
            var enumerated = zippedInput.ToArray();
            if (!enumerated.Any()) throw new ArgumentException(nameof(zippedInput));

            var buffer = new Dictionary<string, string>();
            using (var archive = new ZipArchive(new MemoryStream(enumerated)))
            {
                //ProgressReporter?.StartForIterations(archive.Entries.Count);
                foreach (var entry in archive.Entries)
                {
                    var temp = entry.Open();
                    using (var reader = new StreamReader(temp))
                    {
                        var result = reader.ReadToEnd();
                        buffer.Add(entry.FullName, result);
                    }
                    //ProgressReporter?.ReportProgress();
                }
            }
            return buffer;
        }
        public async Task<Dictionary<string, string>> UnzipAsync(IEnumerable<byte> zippedInput, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            var enumerated = zippedInput.ToArray();
            if (!enumerated.Any()) throw new ArgumentException(nameof(zippedInput));

            var buffer = new Dictionary<string, string>();
            using (var archive = new ZipArchive(new MemoryStream(enumerated)))
            {
                //ProgressReporter?.StartForIterations(archive.Entries.Count);
                foreach (var entry in archive.Entries)
                {
                    var temp = entry.Open();
                    using (var reader = new StreamReader(temp))
                    {
                        var result = await reader.ReadToEndAsync().ConfigureAwait(false);
                        buffer.Add(entry.FullName, result);
                    }
                    //ProgressReporter?.ReportProgress();
                }
            }
            return buffer;
        }
    }
}
