namespace UserManagementAPI.Models
{
    /// <summary>
    /// Class for doctor profile
    /// </summary>
    public class Doctor:DoctorPublicInfo
    {
        /// <summary>
        /// Gets or sets User id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets license
        /// </summary>
        public string License { get; set; }
    }
}
