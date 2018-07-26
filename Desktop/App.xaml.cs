using System;
using System.Windows;
using System.Configuration;
using System.Windows.Threading;
using AuthTokenService;
using Desktop.Views.Windows;
using UserManagementConsumer.Client;

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
        /// Sign In window
        /// </summary>
        private readonly SignIn _signInWindow;

        /// <summary>
        /// Register Window
        /// </summary>
        private readonly RegisterWindow _registerWindow;

        /// <summary>
        /// Gets token provider
        /// </summary>
        public TokenProvider TokenProvider => this._tokenProvider;

        /// <summary>
        /// Gets User Management API client
        /// </summary>
        public UserManagementApiClient UserApiClient => this._userApiClient;

        /// <summary>
        /// Gets SignIn Window 
        /// </summary>
        public SignIn SignInWindow => this._signInWindow;

        /// <summary>
        /// Gets Register window
        /// </summary>
        public RegisterWindow RegisterWindow => this._registerWindow;

        /// <summary>
        /// Creates new instance of <see cref="App"/>
        /// </summary>
        public App()
        {
            // setting fields
            this._tokenProvider = new TokenProvider(
                ConfigurationManager.AppSettings["AuthAPI"],
                int.Parse(ConfigurationManager.AppSettings["UpdateInterval"]));

            this._userApiClient = new UserManagementApiClient(
                ConfigurationManager.AppSettings["UserManagementAPI"]);

            this._signInWindow = new SignIn();
            this._registerWindow = new RegisterWindow();

            // configuring 
            this.ConfigureEventHandlers();
        }

        /// <summary>
        /// Handles window app startup event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event argument</param>
        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            // getting user settings
            var refreshToken = User.Default.RefreshToken;
            var username = User.Default.Username;

            if(string.IsNullOrEmpty(refreshToken) || string.IsNullOrEmpty(username))
            {
                this._signInWindow.Show();
            }
            else
            {
                try
                {
                    var status = await this._tokenProvider.CheckRefreshTokenAsync(refreshToken);

                    if(status == TokenStatus.Error)
                    {
                        this._signInWindow.Show();
                        return;
                    }

                    var response = await this._userApiClient.GetUserByUsernameAsync(username);

                    if (response.Status == Status.Error)
                    {
                        this._signInWindow.Show();
                        return;
                    }

                    User.Default.Id = response.Result.Id;
                    User.Default.Username = response.Result.Username;
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
        }
    }
}
