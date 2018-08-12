using System.Windows;
using System.Windows.Controls;
using Desktop.Services;
using Desktop.ViewModels;
using InstitutionClient.Models;

namespace Desktop.Views.Pages
{
    /// <summary>
    /// Interaction logic for Phamracies.xaml
    /// </summary>
    public partial class Pharmacies : Page
    {
        /// <summary>
        /// Pharmacies view model
        /// </summary>
        public PharmaciesViewModel PharmaciesViewModel { get; private set; }

        /// <summary>
        /// Service for loading pharmacy medicines
        /// </summary>
        public LoadPharmMedicinesService MedicinesService { get; private set; }

        /// <summary>
        /// Service for loading suppliers of given medicine
        /// </summary>
        public LoadSuppliersService SuppliersService { get; private set; }

        /// <summary>
        /// Creates a new instance of <see cref="Pharmacies"/>
        /// </summary>
        public Pharmacies()
        {
            InitializeComponent();

            this.PharmaciesViewModel = new PharmaciesViewModel();
            this.DataContext = this.PharmaciesViewModel;
            this.MedicinesService = new LoadPharmMedicinesService(this.PharmaciesViewModel);
            this.SuppliersService = new LoadSuppliersService(this.PharmaciesViewModel);
        }

        /// <summary>
        /// Click for editing pharmacy
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The Routed Event Args</param>
        private void EditPharmacyClick(object sender, RoutedEventArgs e)
        {
            var id = (int)(sender as Button).Tag;
            this.PharmaciesViewModel.EditablePharmacy = new Institution() { Id = id };

            // Open popup and disable Main Window
            this.EditPopup.IsOpen = true;
            Application.Current.MainWindow.IsEnabled = false;
        }

        /// <summary>
        /// Click for viewing medicines
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The Routed Event Args</param>
        private void ViewMedicinesClick(object sender, RoutedEventArgs e)
        {
            var id = (int)(sender as Button).Tag;
            this.MedicinesService.CurrentPharamcy = id;
            var response =  this.MedicinesService.Load();

            // Open popup and disable Main Window
            this.MedicinesPopup.IsOpen = true;
            Application.Current.MainWindow.IsEnabled = false;
        }

        /// <summary>
        /// Click for closing Edit pharmacy popup
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The Routed Event Args</param>
        private void CloseEditPharmacyPopup_Click(object sender, RoutedEventArgs e)
        {
            this.EditPopup.IsOpen = false;
            Application.Current.MainWindow.IsEnabled = true;
        }

        /// <summary>
        /// Click for closing View medicines popup
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The Routed Event Args</param>
        private void CloseViewMedicinesPopup_Click(object sender, RoutedEventArgs e)
        {
            this.MedicinesPopup.IsOpen = false;
            Application.Current.MainWindow.IsEnabled = true;
        }

        /// <summary>
        /// Pharmacies filter event
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The Routed Event Args</param>
        private async void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Getting the textbox
            var textbox = sender as TextBox;
            
            // Filter pharmacies by appropriate predicate
            if (textbox == this.name)
            {
                await this.PharmaciesViewModel.Filter(pharmacy => pharmacy.Name.Contains(textbox.Text));
            }
            else
            {
                await this.PharmaciesViewModel.Filter(pharmacy => pharmacy.Address.Contains(textbox.Text));
            }
        }

        private async void TextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;

            // Getting the textbox
            var textbox = sender as TextBox;

            this.SuppliersService.MedicineName = textbox.Text;
            await this.SuppliersService.Load();
        }
    }
}
