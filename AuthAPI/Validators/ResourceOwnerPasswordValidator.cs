using System;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using AuthAPI.UsersRepository;
using System.Security.Cryptography;

namespace AuthAPI.Validators
{
    /// <summary>
    /// Resource owner password validatpr
    /// </summary>
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        /// <summary>
        /// User repository
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Constructs new instance of 
        /// <see cref="ResourceOwnerPasswordValidator"/> with the given user repoistory.
        /// </summary>
        /// <param name="userRepository">User Repositor</param>
        public ResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            this._userRepository = userRepository; 
        }

        /// <summary>
        /// Validates context
        /// </summary>
        /// <param name="context">Context</param>
        /// <returns>Validation task.</returns>
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                // getting user
                var user = await this._userRepository.FindAsync(context.UserName);

                // checking password
                if (user != null)
                {
                    // if password is ok set
                    if (this.CheckPassword(context.Password,user.Password) && user.IsVerified)
                    {
                        context.Result = new GrantValidationResult(
                            subject: user.Id.ToString(),
                            authenticationMethod: "custom",
                            claims: GetUserClaims(user));
                        return;
                    }

                    // othwerwise construct error response
                    context.Result = new GrantValidationResult(
                        TokenRequestErrors.InvalidGrant, "Incorrect password");
                    return;
                }
                // message about non-existing user
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant, "User does not exist.");
                return;
            }
            // catching exception
            catch (Exception)
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant, "Invalid username or password");
            }
        }

        /// <summary>
        /// Checks password
        /// </summary>
        /// <param name="password">Password</param>
        /// <param name="hashOfPassword">Hash of Password</param>
        /// <returns>boolean value indicating the validity of password.</returns>
        public bool CheckPassword(string password,string hashOfPassword)
        {
            // extracting the bytes
            var hashBytes = Convert.FromBase64String(hashOfPassword);
            
            // getting salt
            var salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // computing the hash on the password user entered with  password-based ket derivation function 2
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            var hash = pbkdf2.GetBytes(20);

            // comparing hashes
            for (int i = 0; i < 20; i++)
            {
                // return false if there is no-matching hash
                if (hashBytes[i + 16] != hash[i])
                    return false;
            }

            // otherwise return true
            return true;
        }

        /// <summary>
        /// Constructs claims with the given user.
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Claims</returns>
        public static Claim[] GetUserClaims(User user)
        {
            // constructing and returning claims
            return new Claim[]
            {
                new Claim("user_id", user.Id.ToString()),
                new Claim("current_profile",user.CurrentProfileType),
                new Claim(JwtClaimTypes.Name,user.UserName),
            };
        }
    }
}
