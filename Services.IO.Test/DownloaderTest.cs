using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Services.IO.Test
{
    public class DownloaderTest
    {
        private sealed class StubHandler : HttpMessageHandler
        {
            private readonly byte[] _body;
            public StubHandler(byte[] body) => _body = body;
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage _, CancellationToken __)
                => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(_body)
                });
        }

        [Fact]
        public async Task GetBytesAsync_ReturnsResponseBody()
        {
            var expected = new byte[] { 1, 2, 3 };
            var downloader = new Downloader(new HttpClient(new StubHandler(expected)));
            var result = await downloader.GetBytesAsync(new Uri("http://test.local/file"));
            Assert.Equal(expected, result);
        }
    }
}
