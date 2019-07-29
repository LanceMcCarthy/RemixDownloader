using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using RemixDownloader.Core.Models;
using RemixDownloader.Core.Services;
using static System.Console;

namespace RemixDownloader.Console
{
    class Program
    {
        private static HttpClient _client;
        private static CancellationTokenSource _cancellationTokenSource;
        private static CancellationToken _cancellationToken;
        private static bool _hasMoreItems = true;

        public static async Task Main(string[] args)
        {
            WriteLine("Enter the Remix3D User ID (e.g. 46rbnCYv5fy). Press Enter for XBox UserId: ");

            var userId = ReadLine();

            if (string.IsNullOrEmpty(userId))
            {
                userId = "46rbnCYv5fy";
            }

            WriteLine("Enter folder path to save files to (e.g. c:\\Users\\You\\Downloads\\). Press Enter for app directory:");

            var folderPath = ReadLine();

            if (string.IsNullOrEmpty(userId))
            {
                folderPath = Directory.GetCurrentDirectory();
            }

            WriteLine("Do you want to also download optimized HoloLens and WinMR models (Y/N)? This will add to the download time, but saves you a lot of conversion work later! Press Enter for N:");

            var includeOptimizedString = ReadLine()?.ToLower();

            if (string.IsNullOrEmpty(includeOptimizedString))
            {
                includeOptimizedString = "n";
            }

            bool includeOptimized = includeOptimizedString == "y";

            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("Download starting, use CTRL+C to cancel at any time.");

            var userDirectoryPath = Path.Combine(folderPath, userId);
            var userDirectory = Directory.CreateDirectory(userDirectoryPath);

            var handler = new HttpClientHandler();

            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            }

            _client = new HttpClient(handler);

            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;

            CancelKeyPress += Console_CancelKeyPress;

            string contUrl = string.Empty;

            int downloadCount = 0;

            while (_hasMoreItems)
            {
                RemixUserListResponse result;

                if (string.IsNullOrEmpty(contUrl))
                {
                    result = await RemixApiService.Current.GetModelsForUserAsync(userId);
                }
                else
                {
                    result = await RemixApiService.Current.GetModelsForUserAsync(userId, contUrl);
                }

                // We're done! Leave the while loop.
                if (string.IsNullOrEmpty(result.ContinuationUri))
                {
                    _hasMoreItems = false;
                }
                else
                {
                    contUrl = result.ContinuationUri;

                    // This Task will try to download all the files for each of the recently fetched models.
                    await DownloadAllFilesAsync(result.Results, userDirectory, includeOptimized);

                    downloadCount += 10;

                    ForegroundColor = ConsoleColor.Green;
                    WriteLine($"{downloadCount} models downloaded...");
                }
            }

            ForegroundColor = ConsoleColor.Green;
            WriteLine($"DONE.");

            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
            _client.Dispose();
            _client = null;
        }

        private static async Task DownloadAllFilesAsync(IEnumerable<ModelResult> items, DirectoryInfo selectedFolder, bool includeOptimized = false)
        {
            foreach (var item in items)
            {
                try
                {
                    if (_cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    ForegroundColor = ConsoleColor.Yellow;
                    WriteLine($"Downloading {item.Name}...");

                    // *** Phase 1 - Always downloading the original model file *** //

                    // Create a subfolder for each group of files.
                    var modelSubfolder = selectedFolder.CreateSubdirectory(item.Name);

                    // Get the original model file
                    var downloadUrl = item.ManifestUris.FirstOrDefault(u => u.Usage.ToLower() == "download")?.Uri;

                    if (string.IsNullOrEmpty(downloadUrl))
                    {
                        continue;
                    }

                    using (var response = await _client.GetAsync(downloadUrl, _cancellationToken))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var bytes = await response.Content.ReadAsByteArrayAsync();

                            if (bytes == null)
                            {
                                ForegroundColor = ConsoleColor.Red;
                                Debug.WriteLine($"{item.Name} was not downloaded.");
                                continue;
                            }

                            var fileType = item.ManifestUris.FirstOrDefault(u => u.Usage == "Download")?.Format;

                            var fileName = $"{item.Name}.{fileType}";
                            var filePath = Path.Combine(modelSubfolder.FullName, fileName);

                            File.WriteAllBytes(filePath, bytes);

                            ForegroundColor = ConsoleColor.White;
                            SetCursorPosition(CursorLeft, CursorTop - 1);
                            WriteLine();
                            SetCursorPosition(CursorLeft, CursorTop - 1);
                            WriteLine($"Saved {item.Name}.");
                        }
                    }

                    // *** Phase 2 - Download all the optimized versions available *** //

                    // continue if the user doesn't want the extra optimized files, skip to the next loop.
                    if(!includeOptimized)
                        continue;

                    foreach (var optimization in new[] { "Preview", "Performance", "Quality", "HoloLens", "WindowsMR" })
                    {
                        try
                        {
                            // Check cancellation request again
                            if (_cancellationToken.IsCancellationRequested)
                            {
                                break;
                            }

                            ForegroundColor = ConsoleColor.DarkGray;
                            WriteLine($"Downloading {optimization} version of {item.Name}...");

                            // Get the enum of the optimization type
                            var lod = (AssetOptimizationType)Enum.Parse(typeof(AssetOptimizationType), optimization);

                            string extraDownloadUrl = item.AssetUris.FirstOrDefault(u => u.OptimizationType == lod.ToString())?.Uri;

                            if (string.IsNullOrEmpty(extraDownloadUrl))
                            {
                                continue;
                            }

                            using (var response = await _client.GetAsync(extraDownloadUrl, _cancellationToken))
                            {
                                if (!response.IsSuccessStatusCode)
                                {
                                    continue;
                                }

                                var extraFileBytes = await response.Content.ReadAsByteArrayAsync();

                                // If this one failed, move on to next optimization
                                if (extraFileBytes == null || extraFileBytes.Length == 0)
                                {
                                    ForegroundColor = ConsoleColor.Red;
                                    Debug.WriteLine($"{item.Name} - {optimization} was not downloaded.");
                                    continue;
                                }

                                // Get the file extension for that optimization type
                                var extraFileType = item.AssetUris.FirstOrDefault(u => u.OptimizationType == lod.ToString())?.Format;

                                if (string.IsNullOrEmpty(extraFileType))
                                {
                                    continue;
                                }

                                var extraFileName = $"{item.Name} ({lod}).{extraFileType}";
                                var extraFilePath = Path.Combine(modelSubfolder.FullName, extraFileName);

                                File.WriteAllBytes(extraFilePath, extraFileBytes);

                                SetCursorPosition(CursorLeft, CursorTop - 1);
                                WriteLine();
                                SetCursorPosition(CursorLeft, CursorTop - 1);
                                ForegroundColor = ConsoleColor.Yellow;
                                WriteLine($"Saved {item.Name} for {optimization}.");
                            }
                        }
                        catch (Exception ex)
                        {
                            ForegroundColor = ConsoleColor.Red;
                            WriteLine($"Exception getting LOD {item.Name} {optimization} - {ex.Message}");

                            Debug.WriteLine($"Exception getting LOD {item.Name} {optimization} - {ex.Message}");
                        }
                    }

                    ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine($"Completed {item.Name}");

                }
                catch (Exception ex)
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine($"Exception getting {item.Name} - {ex.Message}");

                    Debug.WriteLine($"Exception getting {item.Name} - {ex.Message}");
                }
            }
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine($"Canceling...");

            _cancellationTokenSource.Cancel();

            _hasMoreItems = false;
            e.Cancel = true;
        }
    }
}
