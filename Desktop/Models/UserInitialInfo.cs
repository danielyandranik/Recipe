using System.Collections.Generic;

namespace Desktop.Models
{
    public class UserInitialInfo
    {
        public string Username { get; set; }

        public string FullName { get; set; }

        public string CurrentProfile { get; set; }

        public IEnumerable<string> Profiles { get; set; }
    }
}
