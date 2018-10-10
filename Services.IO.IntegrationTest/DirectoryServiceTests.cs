using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Services.IO.IntegrationTest
{
    public class DirectoryServiceTests
    {
        private const string content1 = "test content /n/r line 2.1,!@#";
        private const string content2 = "test2 content2 2/n/r2 2line2 2.1,!@#2";
        private const string extension = "testextension";
        private const string extensionForNonExistingFiles = "NonExistantExtension";

        [Fact]
        public  void ReadTopDirectoryReturnsEmptyCollectionWhenDirectoryEmpty()
        {
            // Arrange
            var currentDirectory = new DirectoryInfo("./");
            var pattern = $"*.{extension}";
            var filesInDir = currentDirectory.EnumerateFiles(pattern).ToList();
            filesInDir.ForEach(file => file.Delete());

            var tested = new DirectoryService(new FileService());

            // Act

            var actual =  tested.ReadTopDirectory("./", pattern);
            // Assert

            Assert.False(actual.Any());

            // Cleanup
        }

        [Fact]
        public async Task ReadTopDirectoryAsyncReturnsEmptyCollectionWhenDirectoryEmpty()
        {
            // Arrange
            var currentDirectory = new DirectoryInfo("./");
            var pattern = $"*.{extension}";
            var filesInDir = currentDirectory.EnumerateFiles(pattern).ToList();
            filesInDir.ForEach(file => file.Delete());

            var tested = new DirectoryService(new FileService());

            // Act

            var actual = await tested.ReadTopDirectoryAsync("./", pattern);
            // Assert

            Assert.False(actual.Any());

            // Cleanup
        }

        [Fact]
        public async Task ReadTopDirectoryAsyncReadsDirectory()
        {
            // Arrange
            var currentDirectory = new DirectoryInfo("./");
            var pattern = $"*.{extension}";
            var filesInDir = currentDirectory.EnumerateFiles(pattern).ToList();
            filesInDir.ForEach(file => file.Delete());

            var file1 = new FileInfo(Path.ChangeExtension(Path.GetRandomFileName(), extension));
            var file2 = new FileInfo(Path.ChangeExtension(Path.GetRandomFileName(), extension));
            File.WriteAllText(file1.FullName, content1);
            File.WriteAllText(file2.FullName, content2);
            var tested = new DirectoryService(new FileService());
            var expected = new Dictionary<string, string>
            {
                {file1.Name, content1 },
                {file2.Name, content2 },
            };

            // Act

            var actual = await tested.ReadTopDirectoryAsync(file1.Directory.FullName, pattern);
            // Assert

            Assert.Equal(expected, actual);

            // Cleanup
            file1.Delete();
            file2.Delete();
        }

        [Fact]
        public void ReadTopDirectoryReadsDirectory()
        {
            // Arrange
            var currentDirectory = new DirectoryInfo("./");
            var pattern = $"*.{extension}";
            var filesInDir = currentDirectory.EnumerateFiles(pattern).ToList();
            filesInDir.ForEach(file => file.Delete());

            var file1 = new FileInfo(Path.ChangeExtension(Path.GetRandomFileName(), extension));
            var file2 = new FileInfo(Path.ChangeExtension(Path.GetRandomFileName(), extension));
            File.WriteAllText(file1.FullName, content1);
            File.WriteAllText(file2.FullName, content2);
            var tested = new DirectoryService(new FileService());
            var expected = new Dictionary<string, string>
            {
                {file1.Name, content1 },
                {file2.Name, content2 },
            };

            // Act

            var actual =  tested.ReadTopDirectory(file1.Directory.FullName, pattern);
            // Assert

            Assert.Equal(expected, actual);

            // Cleanup
            file1.Delete();
            file2.Delete();
        }
    }
}
