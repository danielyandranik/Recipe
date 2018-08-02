using Desktop.Services;
using System;
using System.Collections.Generic;
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

namespace Desktop.Views.Pages
{
    /// <summary>
    /// Interaction logic for MapPage.xaml
    /// </summary>
    public partial class MapPage : Page
    {
        private readonly PushPinService _pushPinService;

        public MapPage()
        {
            InitializeComponent();
            this._pushPinService = new PushPinService(this.map.Children);
        }

        private async void ComboBoxItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = (ComboBoxItem)sender;

            await this._pushPinService.AddPushPins((string)item.Content);
        }
    }
}
