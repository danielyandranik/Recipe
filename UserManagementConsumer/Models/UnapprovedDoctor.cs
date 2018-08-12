using System;

namespace UserManagementConsumer.Models
{
    /// <summary>
    /// Class for unapproved doctor
    /// </summary>
    public class UnapprovedDoctor
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
        /// Gets or sets license
        /// </summary>
        public string License { get; set; }

        /// <summary>
        /// Gets or sets specification
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// Gets or sets started working date
        /// </summary>
        public string StartedWorking { get; set; }

        /// <summary>
        /// Gets or sets profile created date
        /// </summary>
        public DateTime ProfileCreatedOn { get; set; }
    }
}
