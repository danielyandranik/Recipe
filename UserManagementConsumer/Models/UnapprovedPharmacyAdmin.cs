using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagementConsumer.Models
{
    public class UnapprovedPharmacyAdmin
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        public string PharmacyName { get; set; }

        public string StartedWorking { get; set; }

        public DateTime ProfileCreatedOn { get; set; }
    }
}
