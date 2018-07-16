using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutionAPI.Models
{
    public class MedicineInfo
    {
        public string Name { get; set; }
        
        public string Maker { get; set; }
        
        public string Country { get; set; }
        
        public string Units { get; set; }
        
        public int ShelfLife { get; set; }
        
        public string Description { get; set; }

        public Institution Pharmacy { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
