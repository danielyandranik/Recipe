namespace UserManagementAPI.Models
{
    /// <summary>
    /// Doctor Update Information
    /// </summary>
    public class DoctorUpdateInfo
    {
        /// <summary>
        /// Gets or sets User Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets license
        /// </summary>
        public string License { get; set; }

        /// <summary>
        /// Gets or sets hospital id
        /// </summary>
        public string HospitalName { get; set; }

        /// <summary>
        /// Gets or sets Specification
        /// </summary>
        public string Specification { get; set; }
    }
}
