using Desktop.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Services
{
    public class ProfilesMenuManager
    {
        private readonly MenuItem _menuItem;

        public ProfilesMenuManager(MenuItem menuItem)
        {
            this._menuItem = menuItem;
        }

        public void AddProfiles(IEnumerable<string> profiles)
        {
            var profileTypes = profiles.Select(this.Selector);

            foreach(var profile in profileTypes)
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

                menuItem.Click += async (o, e) =>
                {
                    var item = (MenuItem)o;

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
                    }
                    else
                    {
                        RecipeMessageBox.Show("Error occured");
                    }

                };
       

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
