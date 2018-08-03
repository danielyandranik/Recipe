using System.Windows.Controls;

namespace Desktop.Services
{
    /// <summary>
    /// Navigate service
    /// </summary>
    public class NavigateService
    {
        /// <summary>
        /// Frame
        /// </summary>
        private readonly Frame _frame;

        /// <summary>
        /// Creates new instance of <see cref="NavigateService"/>
        /// </summary>
        /// <param name="frame">Frame</param>
        public NavigateService(Frame frame)
        {
            // setting fields
            this._frame = frame;
        }

        /// <summary>
        /// Navigates to the given page.
        /// </summary>
        /// <typeparam name="TPage">Type of page.</typeparam>
        /// <param name="page">Page</param>
        public void Navigate<TPage>(ref TPage page)  where TPage : Page, new()
        {
            if (page == null)
                page = new TPage();

            this._frame.NavigationService.Navigate(page);
        }
    }
}
