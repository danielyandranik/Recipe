using System.Linq;
using System.Xml.Linq;
using System.Configuration;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;
using Desktop.Views.Windows;
using Desktop.ViewModels;

namespace Desktop.Services
{
    /// <summary>
    /// Profiles menu manager
    /// </summary>
    public class ProfilesMenuManager
    {
        /// <summary>
        /// UI to API current profile types mapping
        /// </summary>
        private readonly Dictionary<string, string> _uiToApi;

        /// <summary>
        /// API to UI current profile types mapping
        /// </summary>
        private readonly Dictionary<string, string> _apiToUi;

        /// <summary>
        /// Menu item
        /// </summary>
        private readonly MenuItem _menuItem;

        /// <summary>
        /// Main window viewmode
        /// </summary>
        private readonly MainWindowViewModel _vm;

        /// <summary>
        /// Gets UI to API current profile types mapping info
        /// </summary>
        public Dictionary<string, string> UiToApi => this._uiToApi;

        /// <summary>
        /// Gets API to UI current profile types mapping info
        /// </summary>
        public Dictionary<string, string> ApiToUi => this._apiToUi;

        /// <summary>
        /// Creates new instance of <see cref="ProfilesMenuManager"/>
        /// </summary>
        /// <param name="menuItem">Menu item</param>
        /// <param name="vm">View model</param>
        public ProfilesMenuManager(MenuItem menuItem,MainWindowViewModel vm)
        {
            // setting fields
            this._menuItem = menuItem;
            this._vm = vm;

            // constructing map info
            this._apiToUi = this.ConstructMapInfo(ConfigurationManager.AppSettings["ApiToUi"]);
            this._uiToApi = this.ConstructMapInfo(ConfigurationManager.AppSettings["UiToApi"]);
        }

        /// <summary>
        /// Add profiles to menu.
        /// </summary>
        /// <param name="profiles">Profiles</param>
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

        /// <summary>
        /// Adds profile to menu item
        /// </summary>
        /// <param name="profile">Profile</param>
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

        /// <summary>
        /// Deletes profile from menu.
        /// </summary>
        /// <param name="profile">profile</param>
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

        /// <summary>
        /// Event handler for menu item click.
        /// After this operation user's current profile updates.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Routed event argument</param>
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

        /// <summary>
        /// Constructs map info
        /// </summary>
        /// <param name="path">path</param>
        /// <returns>map info</returns>
        private Dictionary<string, string> ConstructMapInfo(string path)
        {
            var xml = XDocument.Load(path);

            var items = xml.Element("map").Elements("item");

            return items.ToDictionary(
                item => item.Attribute("key").Value,
                item => item.Attribute("value").Value);
        }

        /// <summary>
        /// Updates button visibilities
        /// </summary>
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

        /// <summary>
        /// Collapses all buttons
        /// </summary>
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
