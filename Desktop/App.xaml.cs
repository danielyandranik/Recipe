using System;
using System.Windows;
using System.Configuration;
using System.Windows.Threading;
using AuthTokenService;
using Desktop.Views.Windows;
using UserManagementConsumer.Client;
using Desktop.Models;
using Desktop.ViewModels;
using Desktop.Services;

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
        /// Handles window app startup event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event argument</param>
        private  async void Application_Startup(object sender, StartupEventArgs e)
        {
            if(!this._isReadyForStartup)
            {
                RecipeMessageBox.Show("Server is not responding.\nUnable to start.");
                return;
            }

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

                    var response = await this._userApiClient.GetUserByUsernameAsync(username);

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
                    RecipeMessageBox.Show("Server is not responding");
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
            RecipeMessageBox.Show("Something went wrong.\nSorry for inconvenience.");
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
    }
}
