using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.IO
{
    public class DirectoryService : IDirectoryService
    {
        private readonly IFileService FileProvider;

        public DirectoryService(IFileService fileService)
        {
            FileProvider = fileService;
        }

        public Dictionary<string, string> ReadTopDirectory(string path, string pattern = "*.*")
        {
            var directory = new DirectoryInfo(path);
            var dataTemp = new Dictionary<string, string>();

            var filesInDir = directory.EnumerateFiles(pattern).ToList();
            if (filesInDir.Count == 0) return dataTemp;
            //readProgress?.StartForIterations(filesInDir.Count);
            foreach (var file in filesInDir)
            {
                if (!dataTemp.ContainsKey(file.Name))
                {
                    dataTemp.Add(file.Name, FileProvider.ReadAllText(file.FullName));
                }
                //readProgress?.ReportProgress();
            }
            return dataTemp;
        }

        public async Task<Dictionary<string, string>> ReadTopDirectoryAsync(string path, string pattern = "*.*", CancellationToken token = default(CancellationToken))
        {
            token.ThrowIfCancellationRequested();

            var directory = new DirectoryInfo(path);
            var dataTemp = new Dictionary<string, string>();

            var filesInDir = directory.EnumerateFiles(pattern).ToList();
            if (filesInDir.Count == 0) return dataTemp;
            //readProgress?.StartForIterations(filesInDir.Count);
            foreach (var file in filesInDir)
            {
                if (!dataTemp.ContainsKey(file.Name))
                {
                    dataTemp.Add(file.Name, await FileProvider.ReadAllTextAsync(file.FullName, token).ConfigureAwait(false));
                }
                //readProgress?.ReportProgress();
            }
            return dataTemp;
        }
    }
}
