using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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

        public void ScrollToItem(ModelResultViewModel item)
        {
            ModelsListView.ScrollIntoView(item);
        }
    }
}