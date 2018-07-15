using System;
using System.Collections.Generic;
using System.Text;

namespace InstitutionAPI.Models
{
    public class Institution
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string License { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        public string OpenTime { get; set; }

        public string CloseTime { get; set; }

        public Address Address { get; set; }

        public string Owner { get; set; }

        public Type Type { get; set; }
    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
}
