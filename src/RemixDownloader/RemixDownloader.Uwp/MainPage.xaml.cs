using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using RemixDownloader.Core.Models;
using RemixDownloader.Core.Services;

namespace RemixDownloader.Uwp
{
    public sealed partial class MainPage : Page
    {
        private readonly ObservableCollection<ModelResult> models;
        private string continuationUri = string.Empty;

        public MainPage()
        {
            this.InitializeComponent();
            models = new ObservableCollection<ModelResult>();
            ModelsGridView.ItemsSource = models;
        }

        private async void Load_OnClick(object sender, RoutedEventArgs e)
        {
            RemixUserListResponse result;

            if (string.IsNullOrEmpty(continuationUri))
            {
                result = await RemixApiService.Current.GetModelsForUserAsync(UserIdTextBox.Text);
            }
            else
            {
                result = await RemixApiService.Current.GetModelsForUserAsync(UserIdTextBox.Text, continuationUri);
            }

            continuationUri = result.ContinuationUri;

            foreach (var item in result.Results)
            {
                models.Add(item);
            }
        }

        private void Reset_OnClick(object sender, RoutedEventArgs e)
        {
            models.Clear();
            continuationUri = string.Empty;
        }
    }
}
