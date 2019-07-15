using CommonHelpers.Common;
using RemixDownloader.Core.Models;

namespace RemixDownloader.Uwp.Models
{
    public class ModelResultViewModel : BindableBase
    {
        private bool isDownloading;
        private bool isSaved;
        private string filePath;
        private string status;

        public ModelResultViewModel(ModelResult model)
        {
            Model = model;
        }

        public ModelResult Model { get; }

        public bool IsDownloading
        {
            get => isDownloading;
            set => SetProperty(ref isDownloading, value);
        }

        public bool IsSaved
        {
            get => isSaved;
            set => SetProperty(ref isSaved, value);
        }

        public string Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }

        public string FilePath
        {
            get => filePath;
            set => SetProperty(ref filePath, value);
        }
    }
}
