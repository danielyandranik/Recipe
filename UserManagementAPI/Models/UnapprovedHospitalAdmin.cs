using System;

namespace UserManagementAPI.Models
{
    public class UnapprovedHospitalAdmin
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        public string HospitalName { get; set; }

        public string StartedWorking { get; set; }

        public DateTime ProfileCreatedOn { get; set; }
    }
}
