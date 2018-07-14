using System;

namespace UserManagementAPI.Models
{
    /// <summary>
    /// Class for user register information
    /// </summary>
    public class UserRegisterInfo
    {
        /// <summary>
        /// Gets or sets first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets middle name
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets full name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets Birthdate
        /// </summary>
        public DateTime Birthdate { get; set; }

        /// <summary>
        /// Gets or sets email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets os sets password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets sex
        /// </summary>
        public string Sex { get; set; }
    }
}
