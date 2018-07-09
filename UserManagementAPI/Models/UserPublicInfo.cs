using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementAPI.Models
{
    public class UserPublicInfo
    {
        /// <summary>
        /// Gets or sets first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets password
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets sex
        /// </summary>
        public string Sex { get; set; }
    }
}
