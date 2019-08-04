using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using CommonHelpers.Common;
using RemixDownloader.Core.Models;
using RemixDownloader.Core.Services;
using RemixDownloader.Uwp.Common;
using RemixDownloader.Uwp.Models;
using Telerik.Core.Data;
using System.Net.Http;
using System.Net;

namespace RemixDownloader.Uwp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly HttpClient client;
        private RemixUserResponse userProfile;
        private IncrementalLoadingCollection<ModelResult> models;
        private ObservableCollection<ModelResultViewModel> selectedModels;
        private string continuationUri = string.Empty;
        private CancellationTokenSource cancellationTokenSource;
        private CancellationToken cancellationToken;
        private StorageFolder preferredDownloadFolder;
        private string boardButtonText = "Get Board Models";
        private List<string> selectedOptionalDownloads;
        private string boardId = "3T5ZY5WEWCn";
        private string enteredUserId = "46rbnCYv5fy";
        private string downloadFolderName = "Select download folder:";

        private bool isReadyForDownload;
        private bool stopDownloadLoop;
        private double downloadProgress;

        public MainViewModel()
        {
            var handler = new HttpClientHandler();

            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            }

            client = new HttpClient(handler);
        }

        public RemixUserResponse UserProfile
        {
            get => userProfile;
            set => SetProperty(ref userProfile, value);
        }

        public ObservableCollection<ModelResultViewModel> SelectedModels
        {
            get => selectedModels ?? (selectedModels = new ObservableCollection<ModelResultViewModel>());
            set => SetProperty(ref selectedModels, value);
        }

        public IncrementalLoadingCollection<ModelResult> Models
        {
            get => models;
            set => SetProperty(ref models, value);
        }

        public List<string> SelectedOptionalDownloads
        {
            get => selectedOptionalDownloads ?? (selectedOptionalDownloads = new List<string>());
            set => SetProperty(ref selectedOptionalDownloads, value);
        }

        public string BoardButtonText
        {
            get => boardButtonText;
            set => SetProperty(ref boardButtonText, value);
        }

        public string BoardId
        {
            get => boardId;
            set => SetProperty(ref boardId, value);
        }

        public string EnteredUserId
        {
            get => enteredUserId;
            set => SetProperty(ref enteredUserId, value);
        }

        public string DownloadFolderName
        {
            get => downloadFolderName;
            set => SetProperty(ref downloadFolderName, value);
        }

        public bool IsReadyForDownload
        {
            get => isReadyForDownload;
            set => SetProperty(ref isReadyForDownload, value);
        }

        public double DownloadProgress
        {
            get => downloadProgress;
            set => SetProperty(ref downloadProgress, value);
        }

        public IScrollToItem ItemScroller { get; set; }

        public async void LoadUser_OnClick(object sender, RoutedEventArgs e)
        {
            BoardButtonText = "Get Board Models";

            // get the Id
            if (string.IsNullOrEmpty(EnteredUserId))
            {
                await new MessageDialog("You need to enter a valid User ID.").ShowAsync();
                return;
            }

            RemixUserResponse result = null;

            try
            {
                result = await RemixApiService.Current.GetUserAsync(EnteredUserId);
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"User ID fetch failed - {exception.Message}");
            }

            if (result == null)
            {
                await new MessageDialog("The User ID you entered is not for a valid or current profile, check the ID (case sensitive!) and try again.").ShowAsync();
            }
            else
            {
                UserProfile = result;

                Models = new IncrementalLoadingCollection<ModelResult>(GetMoreItems) { BatchSize = 10 };
            }
        }

        public async void LoadBoardModels_OnClick(object sender, RoutedEventArgs e)
        {
            // get the Id
            if (string.IsNullOrEmpty(BoardId))
            {
                await new MessageDialog("You need to enter a valid Board ID.").ShowAsync();
                return;
            }

            RemixBoardResponse result;

            if (string.IsNullOrEmpty(continuationUri))
            {
                // Clear the list before loading up a new one
                if (Models.Any())
                {
                    Models.Clear();
                }

                result = await RemixApiService.Current.GetModelsForBoardAsync(BoardId);
            }
            else
            {
                result = await RemixApiService.Current.GetModelsForBoardAsync(BoardId, continuationUri);
            }

            continuationUri = result.Items.ContinuationUri;

            BoardButtonText = "load more...";

            foreach (var item in result.Items.Results)
            {
                Models.Add(item);
            }
        }

        public async void DownloadAllUserModels_OnClick(object sender, RoutedEventArgs e)
        {
            // Make sure there is a valid profile loaded
            if (UserProfile == null)
            {
                await new MessageDialog("You need to first load a Remix3D user profile.").ShowAsync();
                return;
            }

            var md = new MessageDialog("This operation is going to take a while depending on how many models the user has.\r\nDo you want to also download the pre-optimized models for HoloLens and Mixed Reality? This is a nice benefit to take advantage of because you will not have to decimate or improve the model later.", "Include Optimized Models");
            md.Commands.Add(new UICommand("Yes (adds time, but worth it)"));
            md.Commands.Add(new UICommand("No"));
            md.Commands.Add(new UICommand("Cancel"));

            var dialogResult = await md.ShowAsync();

            if (dialogResult.Label == "Cancel")
            {
                return;
            }

            // This is a parameter for the downloader Task
            var includeOptimizedFiles = dialogResult.Label != "No";

            // User selected target folder (the app will create a subfolder for the UserID) 

            var folderPicker = new FolderPicker { SuggestedStartLocation = PickerLocationId.Downloads };
            folderPicker.FileTypeFilter.Add("*");

            var folder = await folderPicker.PickSingleFolderAsync();

            if (folder != null)
            {
                StorageApplicationPermissions.FutureAccessList.AddOrReplace("PreferredAllDownloadsFolderToken", folder);
                StorageApplicationPermissions.MostRecentlyUsedList.AddOrReplace("PreferredAllDownloadsFolderToken", folder);
            }
            else
            {
                // If canceled or folder selection failed
                return;
            }

            // Create a subfolder for the user
            var userSubfolder = await folder.CreateFolderAsync(UserProfile.Id, CreationCollisionOption.OpenIfExists);

            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;

            IsBusy = true;
            IsBusyMessage = "downloading model files...";

            string contUrl = string.Empty;

            int currentDownloadCount = 0;
            int loopNumber = 0;

            while (!stopDownloadLoop)
            {
                try
                {
                    loopNumber++;

                    RemixUserListResponse result;

                    if (string.IsNullOrEmpty(contUrl))
                    {
                        result = await RemixApiService.Current.GetModelsForUserAsync(UserProfile.Id);
                    }
                    else
                    {
                        result = await RemixApiService.Current.GetModelsForUserAsync(UserProfile.Id, contUrl);
                    }

                    if (result != null)
                    {
                        // We're done or the UserID doesn't exist. Leave the while loop.
                        if (string.IsNullOrEmpty(result.ContinuationUri))
                        {
                            if (result.Results.Count > 0)
                            {
                                await DownloadAllFilesAsync(result.Results, userSubfolder, includeOptimizedFiles);
                            }

                            DownloadProgress = 100;

                            stopDownloadLoop = true;
                        }
                        else
                        {
                            // This Task will try to download all the files for each of the recently fetched models.
                            await DownloadAllFilesAsync(result.Results, userSubfolder, includeOptimizedFiles);

                            currentDownloadCount += result.Results.Count;
                            DownloadProgress = currentDownloadCount / (long)UserProfile.CreationCount * 100;

                            contUrl = result.ContinuationUri;
                        }
                    }
                    else
                    {
                        stopDownloadLoop = true;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    IsBusyMessage = $"Error in loop #{loopNumber}.\n\nException Message:\n{ex.Message}.\nInner Exception:\n{ex.InnerException?.Message}";
                }
            }

            DownloadProgress = 0;
            IsBusy = false;
            IsBusyMessage = "done!";
        }

        public void Reset_OnClick(object sender, RoutedEventArgs e)
        {
            Models = null;
            SelectedModels.Clear();
            continuationUri = string.Empty;
            BoardButtonText = "Get Board Models";
        }

        public void ModelsGridView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null)
            {
                foreach (ModelResult model in e.AddedItems)
                {
                    var item = SelectedModels.FirstOrDefault(p => p.Model.Id == model.Id);

                    if (item == null)
                    {
                        SelectedModels.Add(new ModelResultViewModel(model));
                    }
                }
            }

            if (e.RemovedItems != null)
            {
                foreach (ModelResult model in e.RemovedItems)
                {
                    var item = SelectedModels.FirstOrDefault(p => p.Model.Id == model.Id);

                    if (item != null)
                    {
                        SelectedModels.Remove(item);
                    }
                }
            }

            // Enable the download button if there is a selected folder
            if (SelectedModels.Any())
            {
                IsReadyForDownload = preferredDownloadFolder != null;
            }
        }

        public void OptimizationCheckBox_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (sender is CheckBox cb && cb.Tag != null)
            {
                var optimization = cb.Tag.ToString();

                if (!SelectedOptionalDownloads.Contains(optimization))
                {
                    SelectedOptionalDownloads.Add(optimization);
                }
            }
        }

        public void OptimizationCheckBox_Unchecked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (sender is CheckBox cb && cb.Tag != null)
            {
                var optimization = cb.Tag.ToString();

                if (SelectedOptionalDownloads.Contains(optimization))
                {
                    SelectedOptionalDownloads.Remove(optimization);
                }
            }
        }

        public async void SelectTargetFolderButton_OnClick(object sender, RoutedEventArgs e)
        {
            var folderPicker = new FolderPicker { SuggestedStartLocation = PickerLocationId.Downloads };
            folderPicker.FileTypeFilter.Add("*");

            var folder = await folderPicker.PickSingleFolderAsync();

            if (folder != null)
            {
                StorageApplicationPermissions.FutureAccessList.AddOrReplace("PreferredDownloadFolderToken", folder);
                StorageApplicationPermissions.MostRecentlyUsedList.AddOrReplace("PreferredDownloadFolderToken", folder);

                preferredDownloadFolder = folder;
                DownloadFolderName = $"Save To: {preferredDownloadFolder.Name}";
            }
            else
            {
                preferredDownloadFolder = null;
                DownloadFolderName = "No Folder Selected";
            }

            // Enable the download button if there are models ready to download
            if (SelectedModels.Any())
            {
                IsReadyForDownload = preferredDownloadFolder != null;
            }
        }

        public async void DownloadSelectedModels_OnClick(object sender, RoutedEventArgs e)
        {
            if (preferredDownloadFolder == null)
            {
                await new MessageDialog("Select a folder before downloading.").ShowAsync();
                return;
            }

            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;

            await DownloadModelsAsync(preferredDownloadFolder);
        }

        public void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            IsBusyMessage = "Cancelling, please wait...";

            cancellationTokenSource.Cancel();

            stopDownloadLoop = true;

            IsBusyMessage = "Cancelled";

            IsBusy = false;
        }

        private async Task<IEnumerable<ModelResult>> GetMoreItems(uint count)
        {
            RemixUserListResponse result;

            if (string.IsNullOrEmpty(continuationUri))
            {
                result = await RemixApiService.Current.GetModelsForUserAsync(UserProfile.Id);
            }
            else
            {
                result = await RemixApiService.Current.GetModelsForUserAsync(UserProfile.Id, continuationUri);
            }

            // No more items to get
            if (string.IsNullOrEmpty(result.ContinuationUri))
            {
                return null;
            }

            continuationUri = result.ContinuationUri;

            return result.Results;
        }

        private async Task DownloadModelsAsync(StorageFolder folder)
        {
            IsBusy = true;
            IsBusyMessage = "downloading model files...";

            foreach (var item in SelectedModels)
            {
                try
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    // Signals the ListView to scroll to the item that is currently being downloaded
                    ItemScroller.ScrollListViewToItem(item);

                    IsBusyMessage = $"Downloading {item.Model.Name}...";
                    item.Status = "Downloading...";
                    item.IsDownloading = true;

                    // *** Phase 1 - Always downloading the original model file *** //

                    var downloadUrl = item.Model.ManifestUris.FirstOrDefault(u => u.Usage.ToLower() == "download")?.Uri;

                    if (string.IsNullOrEmpty(downloadUrl))
                    {
                        item.Status = "Failed";
                        continue;
                    }

                    using (var response = await client.GetAsync(downloadUrl, cancellationToken))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var bytes = await response.Content.ReadAsByteArrayAsync();

                            if (bytes == null)
                            {
                                Debug.WriteLine($"{item.Model.Name} was not downloaded.");
                                continue;
                            }

                            IsBusyMessage = $"Saving {item.Model.Name}...";
                            item.Status = "saving...";

                            var fileType = item.Model.ManifestUris.FirstOrDefault(u => u.Usage == "Download")?.Format;

                            var fileName = $"{item.Model.Name}.{fileType}";

                            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

                            await FileIO.WriteBytesAsync(file, bytes);

                            item.FilePath = file.Path;
                        }
                    }

                    // *** Phase 2 - if there are any optimized versions selected, download and save them as separate files*** //
                    foreach (var optimization in SelectedOptionalDownloads)
                    {
                        // Check cancellatin request again
                        if (cancellationToken.IsCancellationRequested)
                        {
                            break;
                        }

                        IsBusyMessage = $"Downloading {optimization} version of {item.Model.Name}...";

                        // Get the enum of the optimization type
                        var lod = (AssetOptimizationType)Enum.Parse(typeof(AssetOptimizationType), optimization);

                        string extraDownloadUrl = item.Model.AssetUris.FirstOrDefault(u => u.OptimizationType == lod.ToString())?.Uri;

                        if (string.IsNullOrEmpty(extraDownloadUrl))
                        {
                            continue;
                        }

                        using (var response = await client.GetAsync(extraDownloadUrl, cancellationToken))
                        {
                            if (!response.IsSuccessStatusCode)
                            {
                                continue;
                            }

                            var extraFileBytes = await response.Content.ReadAsByteArrayAsync();

                            // If this one failed, move on to next optimization
                            if (extraFileBytes == null || extraFileBytes.Length == 0)
                            {
                                Debug.WriteLine($"{item.Model.Name} was not downloaded.");
                                continue;
                            }

                            IsBusyMessage = $"Saving {item.Model.Name} for {optimization}...";
                            item.Status = "saving...";

                            // Get the file extension for that optimization type
                            var extraFileType = item.Model.AssetUris.FirstOrDefault(u => u.OptimizationType == lod.ToString())?.Format;

                            if (string.IsNullOrEmpty(extraFileType))
                            {
                                continue;
                            }

                            var extraFileName = $"{item.Model.Name} ({lod}).{extraFileType}";

                            var extraFile = await folder.CreateFileAsync(extraFileName, CreationCollisionOption.ReplaceExisting);

                            await FileIO.WriteBytesAsync(extraFile, extraFileBytes);
                        }

                        // download the file, pasing in the enum so that it uses the correct URL
                        //var extraFileBytes = await RemixApiService.Current.DownloadModelFilesAsync(item.Model, lod, cancellationToken);
                    }

                    item.IsDownloading = false;
                    item.IsSaved = true;
                    item.Status = "Saved";
                }
                catch (Exception ex)
                {
                    if (item.IsDownloading)
                    {
                        item.IsDownloading = false;
                    }

                    item.Status = $"Failed - {ex.Message}";
                }
            }

            IsBusy = false;
            IsBusyMessage = "done!";
        }

        private async Task DownloadAllFilesAsync(IEnumerable<ModelResult> items, StorageFolder selectedFolder, bool includeOptimized = false)
        {
            foreach (var item in items)
            {
                try
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    IsBusyMessage = $"Downloading {item.Name}...";

                    // *** Phase 1 - Always downloading the original model file *** //

                    // Create a subfolder for each group of files.
                    var modelSubfolder = await selectedFolder.CreateFolderAsync(item.Name);

                    // Get the original model file
                    var downloadUrl = item.ManifestUris.FirstOrDefault(u => u.Usage.ToLower() == "download")?.Uri;

                    if (string.IsNullOrEmpty(downloadUrl))
                    {
                        continue;
                    }

                    using (var response = await client.GetAsync(downloadUrl, cancellationToken))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var bytes = await response.Content.ReadAsByteArrayAsync();

                            if (bytes == null)
                            {
                                Debug.WriteLine($"{item.Name} was not downloaded.");
                                continue;
                            }

                            IsBusyMessage = $"Saving {item.Name}...";

                            var fileType = item.ManifestUris.FirstOrDefault(u => u.Usage == "Download")?.Format;

                            var fileName = $"{item.Name}.{fileType}";

                            var file = await modelSubfolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

                            await FileIO.WriteBytesAsync(file, bytes);

                        }
                    }

                    // *** Phase 2 - Download all the optimized versions available *** //

                    // If the user only wants the original file, skip this.
                    if(!includeOptimized)
                        continue;

                    foreach (var optimization in new []{ "Preview", "Performance", "Quality", "HoloLens", "WindowsMR"})
                    {
                        try
                        {
                            // Check cancellation request again
                            if (cancellationToken.IsCancellationRequested)
                            {
                                break;
                            }

                            IsBusyMessage = $"Downloading {optimization} version of {item.Name}...";

                            // Get the enum of the optimization type
                            var lod = (AssetOptimizationType)Enum.Parse(typeof(AssetOptimizationType), optimization);

                            string extraDownloadUrl = item.AssetUris.FirstOrDefault(u => u.OptimizationType == lod.ToString())?.Uri;

                            if (string.IsNullOrEmpty(extraDownloadUrl))
                            {
                                continue;
                            }

                            using (var response = await client.GetAsync(extraDownloadUrl, cancellationToken))
                            {
                                if (!response.IsSuccessStatusCode)
                                {
                                    continue;
                                }

                                var extraFileBytes = await response.Content.ReadAsByteArrayAsync();

                                // If this one failed, move on to next optimization
                                if (extraFileBytes == null || extraFileBytes.Length == 0)
                                {
                                    Debug.WriteLine($"{item.Name} was not downloaded.");
                                    continue;
                                }

                                IsBusyMessage = $"Saving {item.Name} for {optimization}...";

                                // Get the file extension for that optimization type
                                var extraFileType = item.AssetUris.FirstOrDefault(u => u.OptimizationType == lod.ToString())?.Format;

                                if (string.IsNullOrEmpty(extraFileType))
                                {
                                    continue;
                                }

                                var extraFileName = $"{item.Name} ({lod}).{extraFileType}";

                                var extraFile = await modelSubfolder.CreateFileAsync(extraFileName, CreationCollisionOption.ReplaceExisting);

                                await FileIO.WriteBytesAsync(extraFile, extraFileBytes);
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Exception getting LOD {item.Name} {optimization} - {ex.Message}");
                        }
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Exception getting {item.Name} - {ex.Message}");
                }
            }
        }
    }
}
