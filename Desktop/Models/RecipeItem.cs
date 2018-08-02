using MedicineApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Models
{
    public class RecipeItem
    {
        /// <summary>
        /// An identifier of the medicine.
        /// </summary>
        public Medicine Medicine { get; set; }

        /// <summary>
        /// A count of units.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// A unit of frequency.
        /// </summary>
        public string UseFrequencyUnit { get; set; }

        /// <summary>
        /// A Use frequency per frequency unit.
        /// </summary>
        public int TimesPerUnit { get; set; }

        /// <summary>
        /// A count of units per use.
        /// </summary>
        public int CountPerUse { get; set; }
    }
}
