using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Desktop.Views.Windows;

namespace Desktop
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
    {       
        private void Application_Startup(object sender, StartupEventArgs e)
        {

        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            RecipeMessageBox.Show("Something went wrong.\nSorry for inconvenience.");
        }
    }
}
