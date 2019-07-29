using RemixDownloader.Core.Models;
using RemixDownloader.Uwp.Models;

namespace RemixDownloader.Uwp.Common
{
    public interface IScrollToItem
    {
        /// <summary>
        /// Used to scroll the selected models ListView.
        /// </summary>
        /// <param name="item">Item to scroll to.</param>
        void ScrollListViewToItem(ModelResultViewModel item);

        /// <summary>
        /// Used to scroll the user model GridView.
        /// </summary>
        /// <param name="item">Item to scroll to.</param>
        void ScrollGridViewToItem(ModelResult item);
    }
}
