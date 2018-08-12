using System;

namespace UserManagementConsumer.Models
{
    /// <summary>
    /// Class for unapproved hospital admin
    /// </summary>
    public class UnapprovedHospitalAdmin
    {
        /// <summary>
        /// Gets or sets uer id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets fullname
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
