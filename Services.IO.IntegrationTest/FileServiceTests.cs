using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Services.IO.IntegrationTest
{
    public class FileServiceTests
    {
        private const string content = "test content /n/r line 2.1,!@#";

        [Fact]
        public async Task SaveToFileAsyncSavesToFile()
        {
            var file = new FileInfo(Path.GetRandomFileName());
            var tested = new FileService();
            await tested.SaveToFileAsync(file.FullName, content);
            Assert.True(string.Equals(content, File.ReadAllText(file.FullName), StringComparison.InvariantCulture));

            file.Delete();
        }

        [Fact]
        public void SaveToFileSavesToFile()
        {
            var file = new FileInfo(Path.GetRandomFileName());
            var tested = new FileService();
            tested.SaveToFile(file.FullName, content);
            Assert.True(string.Equals(content, File.ReadAllText(file.FullName), StringComparison.InvariantCulture));

            file.Delete();
        }

        [Fact]
        public async Task ReadAllBytesAsyncReadsFile()
        {
            var file = new FileInfo(Path.GetRandomFileName());
            var tested = new FileService();
            File.WriteAllText(file.FullName, content);

            var actual = await tested.ReadAllBytesAsync(file.FullName);
            Assert.Equal(Encoding.ASCII.GetBytes(content), actual);

            file.Delete();
        }

        [Fact]
        public async Task ReadAllLinesAsyncReadsFile()
        {
            var file = new FileInfo(Path.GetRandomFileName());
            var tested = new FileService();
            File.WriteAllText(file.FullName, content);

            var actual = await tested.ReadAllLinesAsync(file.FullName);
            Assert.Equal(content.Split(new[] { Environment.NewLine }, StringSplitOptions.None), actual);

            file.Delete();
        }

        [Fact]
        public void ReadAllBytesReadsFile()
        {
            var file = new FileInfo(Path.GetRandomFileName());
            var tested = new FileService();
            File.WriteAllText(file.FullName, content);

            var actual = tested.ReadAllBytes(file.FullName);
            Assert.Equal(Encoding.ASCII.GetBytes(content), actual);

            file.Delete();
        }

        [Fact]
        public void ReadAllTextReadsFile()
        {
            var file = new FileInfo(Path.GetRandomFileName());
            var tested = new FileService();
            File.WriteAllText(file.FullName, content);

            var actual = tested.ReadAllText(file.FullName);
            Assert.Equal(content, actual);

            file.Delete();
        }

        [Fact]
        public void ReadAllLinesReadsFile()
        {
            var file = new FileInfo(Path.GetRandomFileName());
            var tested = new FileService();
            File.WriteAllText(file.FullName, content);

            var actual = tested.ReadAllLines(file.FullName);
            Assert.Equal(content.Split(new[] { Environment.NewLine }, StringSplitOptions.None), actual);

            file.Delete();

        }
    }
}
