using System;

namespace UserManagementAPI.Models
{
    /// <summary>
    /// Class for unapproved hospital admin
    /// </summary>
    public class UnapprovedHospitalAdmin
    {
        /// <summary>
        /// Gets or sets user id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets full name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets hospital name
        /// </summary>
        public string HospitalName { get; set; }

        /// <summary>
        /// Gets or sets started working date
        /// </summary>
        public string StartedWorking { get; set; }

        /// <summary>
        /// Gets or sets profile creation date
        /// </summary>
        public DateTime ProfileCreatedOn { get; set; }
    }
}
