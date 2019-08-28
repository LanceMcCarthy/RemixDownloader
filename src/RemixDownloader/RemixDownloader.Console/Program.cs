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
using RemixDownloader.Core.Utilities;

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
            System.Console.Title = "Remix3D Downloader";
            
            // XBox User ID
            var defaultUserID = "46rbnCYv5fy";

            UpdateStatus($"Enter the Remix3D User ID (default:'{defaultUserID}' aka XBox):", ConsoleColor.White);

            var userId = System.Console.ReadLine();

            if (string.IsNullOrEmpty(userId))
            {
                userId = defaultUserID;
            }
            else
            {
                userId = userId.Trim();
            }

            UpdateStatus("Enter folder path to save files to (e.g. c:\\Users\\You\\Downloads\\). (default: \\appfolder\\Downloads\\):", ConsoleColor.White);

            var folderPath = System.Console.ReadLine();

            if (string.IsNullOrEmpty(userId))
            {
                folderPath = Path.Combine(Directory.GetCurrentDirectory(),"Downloads");
            }

            //WriteLine("Do you want to also download optimized HoloLens and WinMR models (Y/N)? This will add to the download time, but saves you a lot of conversion work later! Press Enter for N:");
            UpdateStatus("Do you want to also download optimized HoloLens and WinMR models? (default: N)?\r" +
                         "\n- [Y/y] Yes, save the pre-optimized versions models, too (this will save conversion time later)." +
                         "\n- [N/n] No, only save the original model file.", 
                ConsoleColor.White);

            var includeOptimizedString = System.Console.ReadLine()?.ToLower();

            if (string.IsNullOrEmpty(includeOptimizedString))
            {
                includeOptimizedString = "n";
            }

            bool includeOptimized = includeOptimizedString == "y";

            UpdateStatus("Download starting! This will likely take a long time, use CTRL+C to cancel at any time.", ConsoleColor.Green);

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

            System.Console.CancelKeyPress += Console_CancelKeyPress;

            string contUrl = string.Empty;

            int downloadCount = 0;
            int loopNumber = 0;

            while (_hasMoreItems)
            {
                try
                {
                    loopNumber++;

                    RemixUserListResponse result;

                    if (string.IsNullOrEmpty(contUrl))
                    {
                        result = await RemixApiService.Current.GetModelsForUserAsync(userId);
                    }
                    else
                    {
                        result = await RemixApiService.Current.GetModelsForUserAsync(userId, contUrl);
                    }

                    contUrl = result.ContinuationUri;

                    // This Task will try to download all the files for each of the recently fetched models.
                    var numSuccessfulDownloads = await DownloadAllFilesAsync(result.Results, userDirectory, includeOptimized);

                    downloadCount += numSuccessfulDownloads;

                    UpdateStatus($"{downloadCount} models completed...", ConsoleColor.DarkGreen);

                    // We're done! Leave the while loop.
                    if (string.IsNullOrEmpty(result.ContinuationUri))
                    {
                        _hasMoreItems = false;
                    }
                }
                catch (Exception ex)
                {
                    UpdateStatus($"Error in loop #{loopNumber}.\n\nException Message:\n{ex.Message}.\nInner Exception:\n{ex.InnerException?.Message}", ConsoleColor.Red);
                }
            }

            UpdateStatus("DONE!", ConsoleColor.White);

            System.Console.Title = "Remix3D Downloader - Done!";

            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
            _client.Dispose();
            _client = null;
        }

        private static async Task<int> DownloadAllFilesAsync(IEnumerable<ModelResult> items, DirectoryInfo selectedFolder, bool includeOptimized = false)
        {
            var successfulDownloads = 0;

            foreach (var item in items)
            {
                try
                {
                    if (_cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    System.Console.Title = $"Remix3D Downloader - Downloading {item.Name}...";

                    UpdateStatus($"Downloading {item.Name}...", ConsoleColor.DarkYellow);

                    // *** Phase 1 - Always downloading the original model file *** //

                    // Create a subfolder for each group of files.
                    // The ID is suffixed to prevent collisions of models with the same name
                    var modelSubfolder = selectedFolder.CreateSubdirectory($"{item.Name}-{item.Id}");
                    
                    // Get the original model file
                    var downloadUrl = item.ManifestUris.FirstOrDefault(u => u.Usage.ToLower() == "download")?.Uri;

                    if (string.IsNullOrEmpty(downloadUrl))
                    {
                        System.Console.SetCursorPosition(0, System.Console.CursorTop - 1);
                        ClearCurrentConsoleLine();

                        Debug.WriteLine($"{item.Name} downloadUrl was empty, skipping...");
                        continue;
                    }
                    
                    var fileType = item.ManifestUris.FirstOrDefault(u => u.Usage == "Download")?.Format.ToLower();

                    var fileName = $"{item.Name}.{fileType}";

                    var gltfData = await DownloadFile(downloadUrl, _cancellationToken);

                    if (gltfData == null)
                    {
                        UpdateStatus($"{fileName} was not downloaded.", ConsoleColor.DarkRed);
                        continue;
                    }

                    SaveToDisk(modelSubfolder, fileName, gltfData);

                    var gltfModel = GltfHelpers.ParseGltfModel(gltfData);

                    var resourceRootUrl = GltfHelpers.GetGltfResourceRootUrl(downloadUrl);

                    // Extract URIs for resourced referenced by glTF file
                    var bufferUris = gltfModel.Buffers.Select(buffer => buffer.Uri);

                    var imageUris = gltfModel.Images.Select(image => image.Uri);

                    // Combining list of URIs to make progress tracking simpler (& slightly dedupe code)
                    var additionalResourceUrIs = bufferUris.Concat(imageUris).ToArray();

                    var downloadedCount = 0;

                    foreach (var filename in additionalResourceUrIs)
                    {
                        UpdateStatus($"Downloading {downloadedCount+1}/{additionalResourceUrIs.Count()} asset(s)", ConsoleColor.White, true);
                        var targetFileUrl = $"{resourceRootUrl}/{filename}";
                        var bytes = await DownloadFile(targetFileUrl, _cancellationToken);
                        SaveToDisk(modelSubfolder, filename, bytes);
                        downloadedCount++;
                    }
                    
                    successfulDownloads++;

                    UpdateStatus($"Saved {item.Name}.", ConsoleColor.White, true);

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

                            UpdateStatus($"Downloading {optimization} version of {item.Name}...", ConsoleColor.DarkYellow);

                            // Get the enum of the optimization type
                            var lod = (AssetOptimizationType)Enum.Parse(typeof(AssetOptimizationType), optimization);

                            string extraDownloadUrl = item.AssetUris.FirstOrDefault(u => u.OptimizationType == lod.ToString())?.Uri;

                            if (string.IsNullOrEmpty(extraDownloadUrl))
                            {
                                System.Console.SetCursorPosition(0, System.Console.CursorTop - 1);
                                ClearCurrentConsoleLine();
                                continue;
                            }

                            using (var response = await _client.GetAsync(extraDownloadUrl, _cancellationToken))
                            {
                                if (!response.IsSuccessStatusCode)
                                {
                                    System.Console.SetCursorPosition(0, System.Console.CursorTop - 1);
                                    ClearCurrentConsoleLine();
                                    continue;
                                }

                                var extraFileBytes = await response.Content.ReadAsByteArrayAsync();

                                // If this one failed, move on to next optimization

                                if (extraFileBytes == null || extraFileBytes.Length == 0)
                                {
                                    System.Console.SetCursorPosition(0, System.Console.CursorTop - 1);
                                    ClearCurrentConsoleLine();
                                    Debug.WriteLine($"{item.Name} - {optimization} was not saved.");
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

                                UpdateStatus($"Saved {item.Name} for {optimization}.", ConsoleColor.Yellow, true);
                            }
                        }
                        catch (Exception ex)
                        {
                            var errorMessage = $"Exception getting LOD {item.Name} {optimization} - {ex.Message}";

                            UpdateStatus(errorMessage, ConsoleColor.Red);
                            Debug.WriteLine(errorMessage);
                        }
                    }

                    UpdateStatus($"--- {item.Name} complete ---", ConsoleColor.DarkCyan);
                }
                catch (Exception ex)
                {
                    var errorMessage = $"Exception getting {item.Name} - {ex.Message}";

                    UpdateStatus(errorMessage, ConsoleColor.Red);

                    Debug.WriteLine(errorMessage);
                }
            }
            return successfulDownloads;
        }

        private static async Task<byte[]> DownloadFile(string downloadUrl, CancellationToken cancellationToken)
        {
            using (var response = await _client.GetAsync(downloadUrl, cancellationToken))
            {
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Downloading failed; bad HTTP status code - {response.StatusCode}");
                    return null;
                }

                return await response.Content.ReadAsByteArrayAsync();
            }
        }

        private static void SaveToDisk(FileSystemInfo destinationDirectory, string filename, byte[] data)
        {
            var filePath = Path.Combine(destinationDirectory.FullName, filename);

            File.WriteAllBytes(filePath, data);
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            UpdateStatus("Requesting cancel, please wait...", ConsoleColor.Red);

            _cancellationTokenSource.Cancel();

            _hasMoreItems = false;

            e.Cancel = true;

            UpdateStatus("Cancel request complete.", ConsoleColor.White);
        }

        private static void UpdateStatus(string message, ConsoleColor textColor, bool replaceLastLine = false)
        {
            if (replaceLastLine)
            {
                System.Console.SetCursorPosition(0, System.Console.CursorTop - 1);
                ClearCurrentConsoleLine();
            }

            System.Console.ForegroundColor = textColor;
            System.Console.WriteLine(message);
        }

        // Credit https://stackoverflow.com/a/8946847/1406210
        public static void ClearCurrentConsoleLine()
        {
            try
            {
                int currentLineCursor = System.Console.CursorTop;
                System.Console.SetCursorPosition(0, System.Console.CursorTop);
                System.Console.Write(new string(' ', System.Console.BufferWidth));
                System.Console.SetCursorPosition(0, currentLineCursor);
            }
            catch (Exception)
            {
                // ignored -- may fail in some consoles (e.g. VSCode on macOS)
            }
        }
    }
}
