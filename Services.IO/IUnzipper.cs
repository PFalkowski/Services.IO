using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.IO
{
    public interface IUnzipper
    {
        Task<Dictionary<string, string>> UnzipAsync(IEnumerable<byte> zippedInput, CancellationToken cancellationToken);
    }
}