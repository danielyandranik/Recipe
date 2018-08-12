using System;

namespace UserManagementConsumer.Models
{
    /// <summary>
    /// Class for unapproved pharmacies
    /// </summary>
    public class UnapprovedPharmacist
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
        /// Gets or sets pharmacy name
        /// </summary>
        public string PharmacyName { get; set; }

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
