using System;

namespace UserManagementConsumer.Models
{
    public class UserPersonalInfo:UserPublicInfo
    {
        /// <summary>
        /// Gets or sets middle name
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets full name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets birthdate
        /// </summary>
        public DateTime Birthdate { get; set; }

        /// <summary>
        /// Gets or sets email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets password
        /// </summary>
        public string Phone { get; set; }
    }
}
