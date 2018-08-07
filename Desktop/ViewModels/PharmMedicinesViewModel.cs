using Desktop.Models;
using GalaSoft.MvvmLight;
using InstitutionClient.Models;
using System.Collections.ObjectModel;

namespace Desktop.ViewModels
{
    public class PharmMedicinesViewModel : ViewModelBase
    {
        /// <summary>
        /// Container for all pharmacies' medicines
        /// </summary>
        private ObservableCollection<PharmMedicine> pharmMedicines;

        private ObservableCollection<MedicinePricePair> medicinePricePairs;

        public int pharmacyId;

        /// <summary>
        /// Gets or sets pharmacy medicines value 
        /// </summary>
        public ObservableCollection<PharmMedicine> PharmMedicines
        {
            get
            {
                return this.pharmMedicines;
            }
            set
            {
                this.Set("PharmMedicines", ref this.pharmMedicines, value);
            }
        }

        public ObservableCollection<MedicinePricePair> MedicinePricePairs
        {
            get
            {
                return this.medicinePricePairs;
            }
            set
            {
                this.Set("MedicinePricePairs", ref this.medicinePricePairs, value);
            }
        }

        /// <summary>
        /// Creates a new instanse of <see cref="PharmMedicinesViewModel"/>
        /// </summary>
        public PharmMedicinesViewModel()
        {
            
        }
    }
}
