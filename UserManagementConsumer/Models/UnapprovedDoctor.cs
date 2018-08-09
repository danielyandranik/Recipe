using System;

namespace UserManagementConsumer.Models
{
    public class UnapprovedDoctor
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        public string License { get; set; }

        public string Specification { get; set; }

        public string StartedWorking { get; set; }

        public DateTime ProfileCreatedOn { get; set; }
    }
}
