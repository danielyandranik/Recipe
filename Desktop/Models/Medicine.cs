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
        private string _id;
        private string _name;
        private string _maker;
        private string _country;
        private string _units;
        private int _shelfLife;
        private string _description;


        /// <summary>
        /// The identifier of the medicine.
        /// </summary>
        public string Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
                RaisePropertyChanged("Id");
            }
        }

        /// <summary>
        /// Name of the Medicine.
        /// </summary>
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
                RaisePropertyChanged("Name");
            }
        }

        /// <summary>
        /// Who made this product(Company name).
        /// </summary>
        public string Maker
        {
            get
            {
                return this._maker;
            }
            set
            {
                this._maker = value;
                RaisePropertyChanged("Maker");
            }
        }

        /// <summary>
        /// Country where it was made.
        /// </summary>
        public string Country
        {
            get
            {
                return this._country;
            }
            set
            {
                this._country = value;
                RaisePropertyChanged("Country");
            }
        }

        /// <summary>
        /// Units how it counts.
        /// </summary>
        public string Units
        {
            get
            {
                return this._units;
            }
            set
            {
                this._units = value;
                RaisePropertyChanged("Units");
            }
        }

        /// <summary>
        /// Shelf life by mothes.
        /// </summary>
        public int ShelfLife
        {
            get
            {
                return this._shelfLife;
            }
            set
            {
                this._shelfLife = value;
                RaisePropertyChanged("ShelfLife");
            }
        }

        /// <summary>
        /// Information about Medicine.
        /// </summary>
        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
                RaisePropertyChanged("Description");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
