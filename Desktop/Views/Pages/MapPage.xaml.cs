using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Desktop.ViewModels;
using Microsoft.Maps.MapControl.WPF.Core;

namespace Desktop.Views.Pages
{
    /// <summary>
    /// Interaction logic for MapPage.xaml
    /// </summary>
    public partial class MapPage : Page
    {
        private readonly MapPageViewModel _vm;

        public MapPage()
        {
            InitializeComponent();

            this._vm = new MapPageViewModel(this.map.Children);
            this.DataContext = this._vm;
        }
    }
}
