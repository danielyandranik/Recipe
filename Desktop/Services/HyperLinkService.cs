using System.Windows;

namespace Desktop.Services
{
    /// <summary>
    /// Hyper link service
    /// </summary>
    public class HyperLinkService
    {
        /// <summary>
        /// Navigates to another window
        /// </summary>
        /// <typeparam name="T">Type of Window</typeparam>
        public void Navigate<T>() where T:Window,new()
        {
            var newWindow = new T();
            newWindow.Show();
            App.Current.Windows[0].Close();
        }
    }
}
