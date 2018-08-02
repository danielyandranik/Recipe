using System.Collections.Generic;

namespace Desktop.Models
{
    /// <summary>
    /// Class for user initial info
    /// </summary>
    public class UserInitialInfo
    {
        /// <summary>
        /// Gets or sets Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets fullname
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets current profile
        /// </summary>
        public string CurrentProfile { get; set; }

        /// <summary>
        /// Gets or sets profiles
        /// </summary>
        public IEnumerable<string> Profiles { get; set; }
    }
}
