using System.Collections.Generic;
using IdentityServer4.Models;

namespace AuthAPI
{
    /// <summary>
    /// Class for configuration data.
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Gets api resources.
        /// </summary>
        /// <returns>api resources</returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
<<<<<<< HEAD
                new ApiResource("UserManagementAPI"),
                new ApiResource("InstitutionAPI")
=======
<<<<<<< HEAD
                new ApiResource("UserManagementAPI"),
                new ApiResource("RecipeApi")
=======
                new ApiResource("UserManagementAPI"), // new ApiResource("InstitutionAPI")
>>>>>>> 9a1722c5387990bdaaa97cdf306c7b0150101933
>>>>>>> 3daf1758c694fd25986e420c06d1f642850b9b87
            };
        }

        /// <summary>
        /// Gets clients.
        /// </summary>
        /// <returns>clients</returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "RecipeConsoleUI",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
<<<<<<< HEAD
                    AllowedScopes = {"UserManagementAPI", "InstitutionAPI"}
=======
                    AllowedScopes = {"UserManagementAPI", "RecipeApi"}
>>>>>>> 3daf1758c694fd25986e420c06d1f642850b9b87
                }
            };
        }

        /// <summary>
        /// Gets identity resources.
        /// </summary>
        /// <returns>identity resources</returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResource
                {
                    Name = "Username",
                    UserClaims = new List<string> {"Username"}
                }
            };
        }
    }
}
