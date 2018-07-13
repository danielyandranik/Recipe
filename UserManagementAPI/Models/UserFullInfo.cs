namespace UserManagementAPI.Models
{
    /// <summary>
    /// Class for User
    /// </summary>
    public class UserFullInfo:UserPersonalInfo
    {
        /// <summary>
        /// Gets or sets password
        /// </summary>
        public string Password { get; set; }
    }
}
