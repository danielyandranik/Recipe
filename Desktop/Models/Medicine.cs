using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Models
{
    class Medicine : INotifyPropertyChanged
    {
        /// <summary>
        /// The identifier of the medicine.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the Medicine.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Who made this product(Company name).
        /// </summary>
        public string Maker { get; set; }

        /// <summary>
        /// Country where it was made.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Units how it counts.
        /// </summary>
        public string Units { get; set; }

        /// <summary>
        /// Shelf life by mothes.
        /// </summary>
        public int ShelfLife { get; set; }

        /// <summary>
        /// Information about Medicine.
        /// </summary>
        public string Description { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
