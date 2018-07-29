using Desktop.Commands;
using GalaSoft.MvvmLight;
using MedicineApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.ViewModels
{
    class AddMedicineViewModel : ViewModelBase
    {
        private Medicine medicine;

        public Medicine Medicine
        {
            get => this.medicine;
            set => this.Set("Medicine", ref this.medicine, value);
        }

        private AddMedicineCommand addMedicineCommand;

        public AddMedicineViewModel()
        {
            this.medicine = new Medicine();
            this.addMedicineCommand = new AddMedicineCommand(this.addMedicine, _ => true);
        }

        private async Task<bool> addMedicine(Medicine medicine)
        {
            return await ((App)App.Current).MedicineClient.CreateMedicineAsync(medicine);
        }
    }
}
