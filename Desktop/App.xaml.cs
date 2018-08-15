using System;
using System.Linq;
using System.Windows;
using System.Configuration;
using System.Windows.Threading;
using AuthTokenService;
using Desktop.Views.Windows;
using UserManagementConsumer.Client;
using Desktop.Models;
using Desktop.ViewModels;
using Desktop.Services;
using Desktop.Views.Pages;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Desktop
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
    {
        /// <summary>
        /// Token provider
        /// </summary>
        private readonly TokenProvider _tokenProvider;

        /// <summary>
        /// Qr decoder
        /// </summary>
        private  QrDecoderService _qrDecoder;

        /// <summary>
        /// User Management API client
        /// </summary>
        private readonly UserManagementApiClient _userApiClient;

        /// <summary>
        /// Recipe client.
        /// </summary>
        private readonly RecipeClient.RecipeClient _recipeClient;

        /// <summary>
        /// Medicine api client.
        /// </summary>
        private readonly MedicineApiClient.Client _medicineClient;

        /// <summary>
        /// Institution api client.
        /// </summary>
        private readonly InstitutionClient.Client _institutionClient;

        /// <summary>
        /// Profiles menu manager
        /// </summary>
        private ProfilesMenuManager _profilesMenuManager;

        /// <summary>
        /// Boolean value indicating if app is ready for startup
        /// </summary>
        private bool _isReadyForStartup;

        /// <summary>
        /// Map page
        /// </summary>
        private readonly MapPage _mapPage;

        /// <summary>
        /// Handler for profile changed event
        /// </summary>
        public delegate void ProfileChangedHandler();

        /// <summary>
        /// Event for profile changed
        /// </summary>
        public event ProfileChangedHandler ProfileChanged;

        /// <summary>
        /// Gets token provider
        /// </summary>
        public TokenProvider TokenProvider => this._tokenProvider;

        /// <summary>
        /// Gets User Management API client
        /// </summary>
        public UserManagementApiClient UserApiClient => this._userApiClient;

        /// <summary>
        /// Gets profile menu manager
        /// </summary>
        public ProfilesMenuManager ProfilesMenuManager
        {
            get => this._profilesMenuManager;

            set => this._profilesMenuManager = value;
        }

        /// <summary>
        /// Gets or sets qr deocder
        /// </summary>
        public QrDecoderService QrDecoderService
        {
            get => this._qrDecoder;

            set => this._qrDecoder = value;
        }

        /// <summary>
        /// Gets Medicine Api Client
        /// </summary>
        public MedicineApiClient.Client MedicineClient => this._medicineClient;

        /// <summary>
        /// Gets Institution Api Client
        /// </summary>
        public InstitutionClient.Client InstitutionClient => this._institutionClient;

        /// <summary>
        /// Gets the Recipe Api client.
        /// </summary>
        public RecipeClient.RecipeClient RecipeClient => this._recipeClient;

        /// <summary>
        /// Gets or sets map page
        /// </summary>
        public MapPage MapPage => this._mapPage;

        /// <summary>
        /// Creates new instance of <see cref="App"/>
        /// </summary>
        public App()
        {
            try
            {
                // setting fields
                this._tokenProvider = new TokenProvider(
                    ConfigurationManager.AppSettings["AuthAPI"],
                    int.Parse(ConfigurationManager.AppSettings["UpdateInterval"]));

                this._userApiClient = new UserManagementApiClient(
                    ConfigurationManager.AppSettings["UserManagementAPI"]);

                this._recipeClient = new RecipeClient.RecipeClient(ConfigurationManager.AppSettings["RecipeAPI"]);

                this._medicineClient = new MedicineApiClient.Client(ConfigurationManager.AppSettings["MedicineAPI"]);

                this._institutionClient = new InstitutionClient.Client(ConfigurationManager.AppSettings["InstitutionsAPI"]);

                this._mapPage = new MapPage();

                // configuring 
                this.ConfigureEventHandlers();

                this._isReadyForStartup = true;
            }
            catch(Exception)
            {
                this._isReadyForStartup = false;
            }
        }

        /// <summary>
        /// Rises Profile Changed event
        /// </summary>
        public void RiseProfileChanged()
        {
            this.ProfileChanged?.Invoke();

            var main = this.MainWindow as MainWindow;

            main.frame.Navigate(this.MapPage);
        }

        /// <summary>
        /// Handles window app startup event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event argument</param>
        private  async void Application_Startup(object sender, StartupEventArgs e)
        {
            this.CheckBeforeStarting();

            var dictionary = App.Current.Resources;

            if(!this._isReadyForStartup)
            {
                RecipeMessageBox.Show((string)dictionary["start_fail"]);
                return;
            }

            this.LoadLanguagePack();

            // getting user settings
            var refreshToken = User.Default.RefreshToken;
            var username = User.Default.Username;

            if(string.IsNullOrEmpty(refreshToken) || string.IsNullOrEmpty(username))
            {
                new SignIn().Show();
            }
            else
            {
                try
                {
                    var status = await this._tokenProvider.CheckRefreshTokenAsync(refreshToken);

                    if(status == TokenStatus.Error)
                    {
                        new SignIn().Show();
                        return;
                    }

                    var response = await this._userApiClient.GetUserAsync(username);

                    if (response.Status == Status.Error)
                    {
                        new SignIn().Show();
                        return;
                    }

                    User.Default.Id = response.Result.Id;
                    User.Default.Username = response.Result.Username;
                    User.Default.CurrentProfile = response.Result.CurrentProfileType;
                    User.Default.Save();

                    var window = new MainWindow();

                    window.Show();
                }
                catch(Exception)
                {
                    RecipeMessageBox.Show((string)dictionary["server_error"]);
                }
            }
        }

        /// <summary>
        /// Handles unhandled exceptions
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event argument</param>
        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // handling
            e.Handled = true;

            // showing message to the user
            RecipeMessageBox.Show((string)App.Current.Resources["unhandled_exception"]);
        }

        /// <summary>
        /// Updates refresh token in user settings file
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event argument</param>
        private void UpdateRefreshToken(object sender,TokenEventArgs e)
        {
            if (e.RefreshToken == null)
                return;

            User.Default.RefreshToken = e.RefreshToken;
            User.Default.Save();
        }

        /// <summary>
        /// Configures event handlers
        /// </summary>
        private void ConfigureEventHandlers()
        {
            // adding event handlers for TokenUpdated event
            this._tokenProvider.TokenUpdated += this._userApiClient.UpdateToken;
            this._tokenProvider.TokenUpdated += this.UpdateRefreshToken;
            this._tokenProvider.TokenUpdated += this._recipeClient.UpdateToken;
            this._tokenProvider.TokenUpdated += this._medicineClient.UpdateToken;
            this._tokenProvider.TokenUpdated += this._institutionClient.UpdateToken;
        }

        /// <summary>
        /// Loads language pack
        /// </summary>
        private void LoadLanguagePack()
        {
            if (User.Default.Language == "en")
                return;

            var uri = new Uri($"/Language/lang.{User.Default.Language}.xaml", UriKind.Relative);

            var dictionary = new ResourceDictionary
            {
                Source = uri
            };

            var dictionaries = App.Current.Resources.MergedDictionaries;

            dictionaries.RemoveAt(4);
            dictionaries.Add(dictionary);
        }

        /// <summary>
        /// Checks if there is already recipe instance running preventing the new process creation.
        /// </summary>
        private void CheckBeforeStarting()
        {
            var processes = Process.GetProcesses();

            var currentProcess = Process.GetCurrentProcess();

            var runningProcess = processes.Where(process => process.Id != currentProcess.Id && 
                      process.ProcessName.Equals(currentProcess.ProcessName,StringComparison.Ordinal))
                      .FirstOrDefault();

            if (runningProcess == null)
                return;

            // Now calling ShowWindow function from user32.dll.
            // The second parameter of ShowWindow is nCmdShow.
            // We will call it with SW_SHOW parameter which provides the following result:
            // Activates the window and displays it in its current size and position.
            // The value for SH_SHOW is 5.
            // For more information please see docs of user32.dll.
            ShowWindow(runningProcess.MainWindowHandle, 5);

            this.Shutdown();
        }

        /// <summary>
        /// Shows Window
        /// </summary>
        /// <param name="hWnd">Hwnd</param>
        /// <param name="nCmdShow">nCmdShow</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, Int32 nCmdShow);
    }
}
