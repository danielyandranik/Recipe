namespace Desktop.Models
{
    /// <summary>
    /// Class for register information
    /// </summary>
    public class Register
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
        /// Gets or sets sex index
        /// </summary>
        public int SexIndex { get; set; }

        /// <summary>
        /// Gets or sets birthdate day
        /// </summary>
        public string Day { get; set; }

        /// <summary>
        /// Gets or sets birthdate month
        /// </summary>
        public string Month { get; set; }

        /// <summary>
        /// Gets or sets birthdate year
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// Gets or sets mail address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets confirm password
        /// </summary>
        public string ConfirmPassword { get; set; }
    }
}
