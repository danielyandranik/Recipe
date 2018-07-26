using Desktop.Interfaces;
using Desktop.Models;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Services
{
    /// <summary>
    /// Registration service
    /// </summary>
    public class RegistrationService:IService<Response<HttpResponseMessage>>
    {
        /// <summary>
        /// User management API client
        /// </summary>
        private readonly UserManagementApiClient _userManagementApiClient;

        /// <summary>
        /// Creates new instance of <see cref="RegistrationService"/>
        /// </summary>
        public RegistrationService()
        {
            this._userManagementApiClient = ((App)App.Current).UserApiClient;
        }

        /// <summary>
        /// Executes the service
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <returns>result</returns>
        public async Task<Response<HttpResponseMessage>> Execute(object parameter)
        {
            return await this._userManagementApiClient.RegisterAsync(this.Map((Register)parameter));
                
        }

        /// <summary>
        /// Maps the register to register info
        /// </summary>
        /// <param name="register">Register</param>
        /// <returns>User register info</returns>
        private UserRegisterInfo Map(Register register)
        {
            return new UserRegisterInfo
            {
                Username = register.Username,
                Email = register.Email,
                Sex = register.SexIndex == 0 ? "M" : "F",
                FirstName = register.FirstName,
                LastName = register.LastName,
                MiddleName = register.MiddleName,
                Birthdate = $"{register.Year}-{register.Month}-{register.Day}",
                FullName = $"{register.FirstName} {register.MiddleName} {register.LastName}",
                Password = register.Password,
                Phone = register.Phone
            };
        }
    }
}
