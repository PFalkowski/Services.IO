using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Services.IO.Test
{
    public class UnzipperTest
    {
        private static byte[] Input => new byte[] { 80, 75, 3, 4, 20, 0, 0, 0, 0, 0, 249,
                96, 107, 71, 32, 124, 203, 240, 12, 0, 0, 0, 12, 0, 0, 0, 11, 0,
                0, 0, 90, 105, 112, 84, 101, 115, 116, 46, 116, 120, 116, 84, 101,
                115, 116, 90, 105, 112, 112, 70, 105, 108, 101, 80, 75, 1, 2, 20,
                0, 20, 0, 0, 0, 0, 0, 249, 96, 107, 71, 32, 124, 203, 240, 12, 0,
                0, 0, 12, 0, 0, 0, 11, 0, 0, 0, 0, 0, 0, 0, 1, 0, 32, 0, 0, 0, 0,
                0, 0, 0, 90, 105, 112, 84, 101, 115, 116, 46, 116, 120, 116, 80,
                75, 5, 6, 0, 0, 0, 0, 1, 0, 1, 0, 57, 0, 0, 0, 53, 0, 0, 0, 0, 0 };

        private const string ExpectedFileContent = "TestZippFile";

        [Fact]
        public void Unzip_UnzipsCorrectly()
        {
            var tested = new Unzipper();
            var result = tested.Unzip(Input);
            Assert.Equal(ExpectedFileContent, result.First().Value);
        }

        [Fact]
        public void Unzip_NullInput_ThrowsArgumentNullException()
        {
            var tested = new Unzipper();
            Assert.Throws<ArgumentNullException>(() => tested.Unzip(null!));
        }

        [Fact]
        public void Unzip_EmptyInput_ThrowsArgumentException()
        {
            var tested = new Unzipper();
            Assert.Throws<ArgumentException>(() => tested.Unzip(Array.Empty<byte>()));
        }

        [Fact]
        public async Task UnzipAsync_UnzipsCorrectly()
        {
            var tested = new Unzipper();
            var result = await tested.UnzipAsync(Input);
            Assert.Equal(ExpectedFileContent, result.First().Value);
        }

        [Fact]
        public async Task UnzipAsync_NullInput_ThrowsArgumentNullException()
        {
            var tested = new Unzipper();
            await Assert.ThrowsAsync<ArgumentNullException>(() => tested.UnzipAsync(null!));
        }

        [Fact]
        public async Task UnzipAsync_EmptyInput_ThrowsArgumentException()
        {
            var tested = new Unzipper();
            await Assert.ThrowsAsync<ArgumentException>(() => tested.UnzipAsync(Array.Empty<byte>()));
        }

        [Fact]
        public void CancellationTokenCancells()
        {
            var tested = new Unzipper();
            var cancelationSource = new CancellationTokenSource();
            cancelationSource.Cancel();
            Assert.Throws<AggregateException>(() => tested.UnzipAsync(Input, cancelationSource.Token).Result);
        }
    }
}
