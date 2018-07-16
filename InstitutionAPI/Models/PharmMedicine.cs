using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutionAPI.Models
{
    public class PharmMedicine
    {
        public int Id { get; set; }

        public int PharmacyId { get; set; }

        public int MedicineId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
