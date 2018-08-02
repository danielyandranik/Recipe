using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;
using Desktop.Views.Windows;
using Desktop.ViewModels;
using System.Xml.Linq;
using System.Configuration;

namespace Desktop.Services
{
    public class ProfilesMenuManager
    {
        private readonly Dictionary<string, string> _uiToApi;

        private readonly Dictionary<string, string> _apiToUi;

        private readonly MenuItem _menuItem;

        private readonly MainWindowViewModel _vm;

        public Dictionary<string, string> UiToApi => this._uiToApi;

        public Dictionary<string, string> ApiToUi => this._apiToUi;

        public ProfilesMenuManager(MenuItem menuItem,MainWindowViewModel vm)
        {
            this._menuItem = menuItem;
            this._vm = vm;

            this._apiToUi = this.ConstructMapInfo(ConfigurationManager.AppSettings["ApiToUi"]);
            this._uiToApi = this.ConstructMapInfo(ConfigurationManager.AppSettings["UiToApi"]);
        }

        public void AddProfiles(IEnumerable<string> profiles)
        {
            if (profiles == null)
                return;

            var profileTypes = profiles.Select(profile => this._apiToUi[profile]);

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

            menuItem.Click += this.ChangeProfileEventHandler;
        }

        public void DeleteProfile(string profile)
        {
            var menuItems = this._menuItem.Items.Cast<MenuItem>().ToList();

            var count = menuItems.Count();

            var header = this._apiToUi[profile];

            for(var counter = 0; counter < count; counter++)
            {
                if ((string)menuItems[counter].Header == header)
                {
                    this._menuItem.Items.RemoveAt(counter);

                    this._vm.CurrentProfile = "None";

                    this.CollapseAll();

                    User.Default.CurrentProfile = "none";
                    User.Default.Save();

                    this._vm.PhotoUrl = ConfigurationManager.AppSettings[this._vm.CurrentProfile];

                    break;
                }
            }
        }

        public async void ChangeProfileEventHandler(object sender, RoutedEventArgs e)
        {
            var item = (MenuItem)sender;

            if ((string)item.Header == this._apiToUi[User.Default.CurrentProfile])
                return;

            var client = ((App)App.Current).UserApiClient;

            var currentProfile = (string)item.Header;

            var profileUpdateInfo = new ProfileUpdateInfo
            {
                Id = User.Default.Id,
                Profile = this._uiToApi[currentProfile]
            };

            var response = await client.UpdateCurrentProfileAsync(profileUpdateInfo);

            if (response.Status == Status.Ok)
            {
                this._vm.CurrentProfile = currentProfile;

                User.Default.CurrentProfile = profileUpdateInfo.Profile;
                User.Default.Save();

                this.UpdateButtonsVisibilities();

                this._vm.PhotoUrl = ConfigurationManager.AppSettings[User.Default.CurrentProfile];

                RecipeMessageBox.Show("Current profile is updated");               
            }
            else
            {
                RecipeMessageBox.Show("Error occured.\nMaybe your profile is not approved yet.");
            }
        }

        private Dictionary<string, string> ConstructMapInfo(string path)
        {
            var xml = XDocument.Load(path);

            var items = xml.Element("map").Elements("item");

            return items.ToDictionary(
                item => item.Attribute("key").Value,
                item => item.Attribute("value").Value);
        }

        public void UpdateButtonsVisibilities()
        {
            var currentProfile = User.Default.CurrentProfile;

            this._vm.CreateRecipeVisibility = (currentProfile == "doctor") ? Visibility.Visible : Visibility.Collapsed;
            this._vm.SellMedicinesVisibility = (currentProfile == "pharmacist") ? Visibility.Visible : Visibility.Collapsed;
            this._vm.AddMedicineVisibility = (currentProfile == "ministry_worker") ? Visibility.Visible : Visibility.Collapsed;
            this._vm.AddInstitutionVisibility = (currentProfile == "ministry_worker") ? Visibility.Visible : Visibility.Collapsed;
            this._vm.MyApprovalsVisibility = (currentProfile == "doctor" || currentProfile == "ministry_worker") ? Visibility.Visible : Visibility.Collapsed;
            this._vm.MyRecipesVisibility = (currentProfile == "patient") ? Visibility.Visible : Visibility.Collapsed;
            this._vm.DeleteVisibility = Visibility.Visible;
        }

        public void CollapseAll()
        {
            this._vm.CreateRecipeVisibility = Visibility.Collapsed;
            this._vm.SellMedicinesVisibility = Visibility.Collapsed;
            this._vm.AddMedicineVisibility = Visibility.Collapsed;
            this._vm.AddInstitutionVisibility = Visibility.Collapsed;
            this._vm.MyApprovalsVisibility = Visibility.Collapsed;
            this._vm.MyRecipesVisibility = Visibility.Collapsed;
            this._vm.DeleteVisibility = Visibility.Collapsed;
        }
    }
}
