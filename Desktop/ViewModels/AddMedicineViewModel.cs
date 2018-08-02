using System.Threading.Tasks;
using Desktop.Commands;
using GalaSoft.MvvmLight;
using MedicineApiClient;

namespace Desktop.ViewModels
{
    class AddMedicineViewModel : ViewModelBase
    {
        private Medicine medicine;

        private readonly AddMedicineCommand _addMedicineCommand;

        public Medicine Medicine
        {
            get => this.medicine;
            set => this.Set("Medicine", ref this.medicine, value);
        }

        public AddMedicineCommand AddMedicineCommand => this._addMedicineCommand;

        public AddMedicineViewModel()
        {
            this.medicine = new Medicine();
            this._addMedicineCommand = new AddMedicineCommand(this.AddMedicine, _ => true);
        }

        private async Task<bool> AddMedicine(Medicine medicine)
        {
            return await ((App)App.Current).MedicineClient.CreateMedicineAsync(medicine);
        }
    }
}
