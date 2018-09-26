using System;
using System.Threading;
using System.Threading.Tasks;

namespace Services.IO
{
    public interface IDownloader
    {
        Task<byte[]> GetBytesAsync(Uri uniqueResourceIdentifier, CancellationToken token);
    }
}
