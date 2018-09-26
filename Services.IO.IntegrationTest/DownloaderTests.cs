using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Services.IO.Test
{
    public class DownloaderTests
    {

        ////[Fact]
        //public void DownloadManagerGetRealStockData()
        //{
        //    const string example = "http://bossa.pl/pub/metastock/mstock/mstall.zip";
        //    var tested = new DownloadService(example);
        //    byte[] result;
        //    result = tested.GetBytesAsync().Result;
        //    Assert.IsTrue(result.Count() > 19000000);
        //}
        //[Fact]
        //public void WebClientDownloaderGetBytesAsyncTest()
        //{
        //    const string example = "http://example.com";
        //    var tested = new WebClientDownloader();
        //    var result = tested.GetBytesAsync(new Uri(example), new CancellationToken()).Result;
        //    Assert.IsTrue(result.Length > 100);
        //}
        [Fact]
        public async Task DownloaderDownloadsContent()
        {
            const string example = "http://example.com";
            var tested = new Downloader();
            var result = await tested.GetBytesAsync(new Uri(example));
            Assert.True(result.Length > 100);
        }
        [Fact]
        public void CancellationTokenCancells()
        {
            const string example = "http://example.com";
            var tested = new Downloader();
            var cancelationSource = new CancellationTokenSource();
            cancelationSource.Cancel();
            Assert.Throws<AggregateException>(() => tested.GetBytesAsync(new Uri(example), cancelationSource.Token).Result);
        }
    }
}
