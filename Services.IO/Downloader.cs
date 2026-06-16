using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Services.IO
{
    public class Downloader : IDownloader
    {
        private static readonly HttpClient _defaultClient = new HttpClient();
        private readonly HttpClient _client;

        public Downloader() => _client = _defaultClient;

        internal Downloader(HttpClient client) => _client = client;

        public async Task<byte[]> GetBytesAsync(Uri requestUri, CancellationToken cancellationToken = default)
        {
            using (var response = await _client.GetAsync(requestUri, cancellationToken).ConfigureAwait(false))
            {
                return await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            }
        }
    }
}
