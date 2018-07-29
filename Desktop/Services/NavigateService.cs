using System.Windows.Controls;

namespace Desktop.Services
{
    public class NavigateService
    {
        private readonly Frame _frame;

        public NavigateService(Frame frame)
        {
            this._frame = frame;
        }

        public void Navigate<TPage>(TPage page)  where TPage : Page, new()
        {
            if (page == null)
                page = new TPage();

            this._frame.NavigationService.Navigate(page);
        }
    }
}
