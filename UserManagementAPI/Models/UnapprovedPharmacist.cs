using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementAPI.Models
{
    public class UnapprovedPharmacist
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        public string PharmacyName { get; set; }

        public string StartedWorking { get; set; }

        public DateTime ProfileCreatedOn { get; set; }
    }
}
