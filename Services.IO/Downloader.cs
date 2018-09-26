using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Services.IO
{
    public class Downloader : IDownloader
    {
        public async Task<byte[]> GetBytesAsync(Uri requestUri, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var client = new HttpClient())
            using (var response = await client.GetAsync(requestUri, cancellationToken).ConfigureAwait(false))
            {
                var data = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                return data;
            }
        }
    }
}
