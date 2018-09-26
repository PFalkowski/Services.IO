using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.IO
{
    public interface IDirectoryService
    {
        Dictionary<string, string> ReadTopDirectory(string path, string pattern = "*.*");
        Task<Dictionary<string, string>> ReadTopDirectoryAsync(string path, string pattern = "*.*", CancellationToken token = default(CancellationToken));
    }
}
