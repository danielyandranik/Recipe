using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;
using Desktop.Views.Windows;
using Desktop.ViewModels;

namespace Desktop.Services
{
    public class ProfilesMenuManager
    {
        private readonly MenuItem _menuItem;

        private readonly MainWindowViewModel _vm;

        public ProfilesMenuManager(MenuItem menuItem,MainWindowViewModel vm)
        {
            this._menuItem = menuItem;
            this._vm = vm;
        }

        public void AddProfiles(IEnumerable<string> profiles)
        {
            if (profiles == null)
                return;

            var profileTypes = profiles.Select(this.Selector);

            foreach(var profile in profileTypes)
            {
                this.AddProfile(profile);
            }
        }

        public void AddProfile(string profile)
        {
            var menuItem = new MenuItem
            {
                Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x39, 0xB9, 0xB9)),
                Foreground = Brushes.White,
                FontFamily = new FontFamily("Comfortaa"),
                FontSize = 18,
                Header = profile
            };

            this._menuItem.Items.Add(menuItem);

            menuItem.Click += this.AddProfileEventHandler;
        }

        public void DeleteProfile(string profile)
        {
            var menuItems = this._menuItem.Items.Cast<MenuItem>().ToList();

            var count = menuItems.Count();

            for(var counter = 0; counter < count; counter++)
            {
                if ((string)menuItems[counter].Header == profile)
                {
                    this._menuItem.Items.RemoveAt(counter);

                    RecipeMessageBox.Show("Profile is deleted");

                    break;
                }
            }

            RecipeMessageBox.Show("Error occured");
        }

        public async void AddProfileEventHandler(object sender, RoutedEventArgs e)
        {
            var item = (MenuItem)sender;

            if ((string)item.Header == this.Selector(User.Default.CurrentProfile))
                return;

            var client = ((App)App.Current).UserApiClient;

            var profileUpdateInfo = new ProfileUpdateInfo
            {
                Id = User.Default.Id,
                Profile = (string)item.Header
            };

            var response = await client.UpdateCurrentProfileAsync(profileUpdateInfo);

            if (response.Status == Status.Ok)
            {
                RecipeMessageBox.Show("Current profile is updated");
                this._vm.CurrentProfile = profileUpdateInfo.Profile;
            }
            else
            {
                RecipeMessageBox.Show("Error occured");
            }
        }

        public string Selector(string profile)
        {
            if (profile == "doctor")
                return "Doctor";
            else if (profile == "patient")
                return "Patient";
            else if (profile == "hospital_director")
                return "Hospital Admin";
            else if (profile == "pharmacy_admin")
                return "Pharmacy Admin";
            else if (profile == "pharmacist")
                return "Pharmacist";
            else
                return "Ministry Worker";
        }
    }
}
