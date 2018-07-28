using System;
using System.Linq;
using System.Windows;

namespace Desktop.Services
{
    /// <summary>
    /// Hyper link service
    /// </summary>
    public class HyperLinkService
    {
        /// <summary>
        /// Navigates between the windows.
        /// </summary>
        /// <typeparam name="TFrom">Type of the window to navigate from.</typeparam>
        /// <typeparam name="TTo">Type of the window to navigate to</typeparam>
        public void Navigate<TFrom,TTo>() 
            where TFrom:Window
            where TTo:Window,new()
        {
            // opening new window
            var window = new TTo();
            window.Show();

            // closing the last window
            var closingWindow = App.Current.Windows.OfType<TFrom>().Last();
            closingWindow.Close();
        }

        public void Navigate<TFrom,TTo,TToParameter>(TToParameter toParameter)
            where TFrom:Window
            where TTo: Window
        {
            var window = (TTo)Activator.CreateInstance(typeof(TTo), toParameter);

            window.Show();
            App.Current.Windows.OfType<TFrom>().Last().Close();
        }
    }
}
