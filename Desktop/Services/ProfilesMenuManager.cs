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
using AuthTokenService;

namespace Desktop.Services
{
    /// <summary>
    /// Profiles menu manager
    /// </summary>
    public class ProfilesMenuManager
    {
        /// <summary>
        /// Menu item
        /// </summary>
        private readonly MenuItem _menuItem;

        /// <summary>
        /// Main window viewmode
        /// </summary>
        private readonly MainWindowViewModel _vm;

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
        }

        /// <summary>
        /// Add profiles to menu.
        /// </summary>
        /// <param name="profiles">Profiles</param>
        public void AddProfiles(IEnumerable<string> profiles)
        {
            if (profiles == null)
                return;

            foreach(var profile in profiles)
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
                FontSize = 18
            };

            menuItem.SetResourceReference(MenuItem.HeaderProperty, profile);

            menuItem.Click += this.ChangeProfileEventHandler;

            this._menuItem.Items.Add(menuItem);
        }

        /// <summary>
        /// Deletes profile from menu.
        /// </summary>
        /// <param name="profile">profile</param>
        public void DeleteProfile(string profile)
        {
            var menuItems = this._menuItem.Items.Cast<MenuItem>().ToList();

            var count = menuItems.Count();

            var dictionary = App.Current.Resources;

            var header = (string)dictionary[profile];

            for(var counter = 0; counter < count; counter++)
            {
                if ((string)menuItems[counter].Header == header)
                {
                    this._menuItem.Items.RemoveAt(counter);

                    var deletingMenuItem = (MenuItem)menuItems[counter];

                    deletingMenuItem.Click -= this.ChangeProfileEventHandler;

                    this._vm.CurrentProfile = (string)dictionary["none"];
                                
                    this.CollapseAll();

                    User.Default.CurrentProfile = "none";
                    User.Default.Save();

                    this._vm.PhotoUrl = ConfigurationManager.AppSettings[User.Default.CurrentProfile];

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

            var dictionary = App.Current.Resources;

            if (item.Header == dictionary[User.Default.CurrentProfile])
                return;

            var client = ((App)App.Current).UserApiClient;

            var currentProfile = (string)item.Header;

            var profileUpdateInfo = new ProfileUpdateInfo
            {
                Id = User.Default.Id,
                Profile = (string)dictionary[currentProfile]
            };

            var response = await client.UpdateCurrentProfileAsync(profileUpdateInfo);

            if (response.Status == Status.Ok)
            {
                this._vm.CurrentProfile = currentProfile;

                User.Default.CurrentProfile = profileUpdateInfo.Profile;
                User.Default.Save();

                this.UpdateButtonsVisibilities();

                this._vm.PhotoUrl = ConfigurationManager.AppSettings[User.Default.CurrentProfile];

                var app = ((App)App.Current);

                var tokenProvider = app.TokenProvider;

                var tokenResponse = await tokenProvider.RefreshAccessTokenAsync();

                if (tokenResponse == TokenStatus.Error)
                    return;

                app.RiseProfileChanged();

                RecipeMessageBox.Show((string)dictionary["current_update_success"]);               
            }
            else
            {
                RecipeMessageBox.Show((string)dictionary["current_update_fail"]);
            }
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
            this._vm.MyApprovalsVisibility = (currentProfile == "hospital_director" || currentProfile == "ministry_worker") ? Visibility.Visible : Visibility.Collapsed;
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
