using Windows.UI.Xaml.Controls;
using RemixDownloader.Core.Models;
using RemixDownloader.Uwp.Common;
using RemixDownloader.Uwp.Models;

namespace RemixDownloader.Uwp
{
    public sealed partial class MainPage : Page, IScrollToItem
    {
        public MainPage()
        {
            InitializeComponent();
            ViewModel.ItemScroller = this;
        }

        public void ScrollListViewToItem(ModelResultViewModel item)
        {
            SelectedModelsListView.ScrollIntoView(item);
        }

        public void ScrollGridViewToItem(ModelResult item)
        {
            ModelsGridView.ScrollIntoView(item);
        }
    }
}