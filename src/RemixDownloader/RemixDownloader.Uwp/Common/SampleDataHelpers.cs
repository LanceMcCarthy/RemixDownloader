using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Storage;
using RemixDownloader.Core.Models;

namespace RemixDownloader.Uwp.Common
{
    public static class SampleDataHelpers
    {
        public static async Task<ObservableCollection<ModelResult>> GetDesignTimeUserDataAsync()
        {
            var jsonFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/SampleData/ExampleUserResponse.json"));

            string json = await FileIO.ReadTextAsync(jsonFile);

            var result = RemixUserListResponse.FromJson(json);

            return new ObservableCollection<ModelResult>(result.Results);
        }

        public static async Task<ObservableCollection<ModelResult>> GetDesignTimeBoardDataAsync()
        {
            var jsonFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/SampleData/ExampleBoardResponse.json"));

            string json = await FileIO.ReadTextAsync(jsonFile);

            var result = RemixBoardResponse.FromJson(json);

            return new ObservableCollection<ModelResult>(result.Items.Results);
        }
    }
}
