namespace UserManagementAPI.Models
{
    /// <summary>
    /// Class for password update information
    /// </summary>
    public class PasswordUpdateInfo
    {
        /// <summary>
        /// Gets or sets Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets old password
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// Gets or sets new password
        /// </summary>
        public string NewPassword { get; set; }
    }
}
