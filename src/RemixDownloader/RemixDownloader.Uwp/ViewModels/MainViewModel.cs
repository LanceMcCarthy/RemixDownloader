using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using CommonHelpers.Common;
using CommonHelpers.Extensions;
using RemixDownloader.Core.Models;
using RemixDownloader.Core.Services;
using RemixDownloader.Uwp.Common;
using RemixDownloader.Uwp.Models;

namespace RemixDownloader.Uwp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<ModelResult> models;
        private ObservableCollection<ModelResultViewModel> selectedModels;
        private string continuationUri = string.Empty;
        private CancellationTokenSource cancellationTokenSource;
        private CancellationToken cancellationToken;
        private string boardButtonText = "Get Board Models";
        private string userButtonText = "Get User Models";
        private string selectedOptimizationOption;

        private string boardId = "3T5ZY5WEWCn";
        private string userId = "46rbnCYv5fy";

        public MainViewModel()
        {
            //if (DesignMode.DesignModeEnabled || DesignMode.DesignMode2Enabled)
            //{
            //    Models = SampleDataHelpers.GetDesignTimeUserDataAsync().Result;
            //}

            SelectedOptimizationOption = ModelOptimizationOptions[1];
        }

        public ObservableCollection<ModelResult> Models
        {
            get => models ?? (models = new ObservableCollection<ModelResult>());
            set => SetProperty(ref models, value);
        }

        public ObservableCollection<ModelResultViewModel> SelectedModels
        {
            get => selectedModels ?? (selectedModels = new ObservableCollection<ModelResultViewModel>());
            set => SetProperty(ref selectedModels, value);
        }

        public List<string> ModelOptimizationOptions { get; } = new List<string> { "OriginalView", "OriginalDownload", "Preview", "Performance", "Quality", "HoloLens", "WindowsMR" };

        public string SelectedOptimizationOption
        {
            get => selectedOptimizationOption;
            set => SetProperty(ref selectedOptimizationOption, value);
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

        public string UserButtonText
        {
            get => userButtonText;
            set => SetProperty(ref userButtonText, value);
        }

        public string UserId
        {
            get => userId;
            set => SetProperty(ref userId, value);
        }

        public IScrollToItem ItemScroller { get; set; }

        public async void LoadBoardModels_OnClick(object sender, RoutedEventArgs e)
        {
            // Reset other button
            UserButtonText = "Get User Models";

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

        public async void LoadUserModels_OnClick(object sender, RoutedEventArgs e)
        {
            BoardButtonText = "Get Board Models";

            // get the Id
            if (string.IsNullOrEmpty(UserId))
            {
                await new MessageDialog("You need to enter a valid User ID.").ShowAsync();
                return;
            }

            RemixUserListResponse result;

            if (string.IsNullOrEmpty(continuationUri))
            {
                // Clear the list before loading up a new one
                if (Models.Any())
                {
                    Models.Clear();
                }

                result = await RemixApiService.Current.GetModelsForUserAsync(UserId);
            }
            else
            {
                result = await RemixApiService.Current.GetModelsForUserAsync(UserId, continuationUri);
            }

            continuationUri = result.ContinuationUri;

            UserButtonText = "load more...";

            foreach (var item in result.Results)
            {
                Models.Add(item);
            }
        }

        public void Reset_OnClick(object sender, RoutedEventArgs e)
        {
            Models.Clear();
            SelectedModels.Clear();
            continuationUri = string.Empty;
            BoardButtonText = "Get Board Models";
            UserButtonText = "Get User Models";
        }

        public void ModelsGridView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null)
            {
                foreach (ModelResult model in e.AddedItems)
                {
                    var item = SelectedModels.FirstOrDefault(p=>p.Model.Id == model.Id);

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
        }

        public async void Download_OnClick(object sender, RoutedEventArgs e)
        {
            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;

            var lod = (AssetOptimizationType)Enum.Parse(typeof(AssetOptimizationType), SelectedOptimizationOption);

            await DownloadModelsAsync(lod);
        }

        public void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            cancellationTokenSource.Cancel();
            IsBusy = false;
            IsBusyMessage = "Cancelled";
        }

        private async Task DownloadModelsAsync(AssetOptimizationType levelOfDetail)
        {
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
                    ItemScroller.ScrollToItem(item);

                    IsBusyMessage = $"Downloading {item.Model.Name}...";
                    item.Status = "downloading...";
                    item.IsDownloading = true;

                    var bytes = await RemixApiService.Current.DownloadModelFilesAsync(item.Model, levelOfDetail, cancellationToken);

                    item.IsDownloading = false;

                    if (bytes == null)
                    {
                        Debug.WriteLine($"{item.Model.Name} was not downloaded.");
                        continue;
                    }

                    string fileType = string.Empty;

                    if (levelOfDetail == AssetOptimizationType.OriginalView || levelOfDetail == AssetOptimizationType.OriginalDownload)
                    {
                        fileType = item.Model.ManifestUris.FirstOrDefault(u => u.Usage == "View")?.Format;
                    }
                    else if (levelOfDetail == AssetOptimizationType.OriginalDownload)
                    {
                        fileType = item.Model.ManifestUris.FirstOrDefault(u => u.Usage == "Download")?.Format;
                    }
                    else
                    {
                        fileType = item.Model.AssetUris.FirstOrDefault(u => u.OptimizationType == levelOfDetail.ToString())?.Format;
                    }

                    IsBusyMessage = $"Saving {item.Model.Name}...";
                    item.Status = "saving...";

                    item.FilePath = await bytes.SaveToLocalFolderAsync($"{item.Model.Name}.{fileType}");

                    item.IsSaved = true;
                    item.Status = "Saved";
                }
                catch (Exception ex)
                {
                    if(item.IsDownloading)
                    {
                        item.IsDownloading = false;
                    }

                    item.Status = $"{ex.Message}";
                }
            }

            IsBusy = false;
            IsBusyMessage = "done!";
        }
    }
}
