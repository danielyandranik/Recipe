namespace UserManagementAPI.Models
{
    /// <summary>
    /// Class for approvals
    /// </summary>
    public class Approval
    {
        /// <summary>
        /// Gets or sets user id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets type
        /// </summary>
        public string Type { get; set; }
    }
}
