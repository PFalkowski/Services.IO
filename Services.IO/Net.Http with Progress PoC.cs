        //public async Task<byte[]> GetBytesAsync(string requestUri, CancellationToken cancellationToken = default(CancellationToken), IProgress<double> progress = null, HttpClient client = null)
        //{
        //    using (var response = await client.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead))
        //    {
        //        var contentLength = response.Content.Headers.ContentLength;

        //        using (var download = await response.Content.ReadAsStreamAsync())
        //        {
        //            if (progress != null && contentLength.HasValue)
        //            {
        //                const int bufferSize = 4096;
        //                var buffer = new byte[bufferSize];
        //                long totalBytesRead = 0;
        //                int bytesRead;
        //                using (var destination = new MemoryStream())
        //                {
        //                    while ((bytesRead = await download.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) != 0)
        //                    {
        //                        var relativeProgress = new Progress<long>(totalBytes => progress.Report((float)totalBytes / contentLength.Value));

        //                        await destination.WriteAsync(buffer, 0, bytesRead, cancellationToken).ConfigureAwait(false);
        //                        totalBytesRead += bytesRead;
        //                        progress?.Report(totalBytesRead);
        //                    }
        //                    progress.Report(1);
        //                    return destination.ToArray();
        //                }
        //            }
        //            else
        //            {
        //                return await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
        //            }
        //        }
        //    }
        //}