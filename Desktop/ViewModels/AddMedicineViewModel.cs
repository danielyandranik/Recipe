using Desktop.Commands;
using GalaSoft.MvvmLight;
using MedicineApiClient;
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

        public AddMedicineCommand AddMedicineCommand { get; }

        public AddMedicineViewModel()
        {
            this.medicine = new Medicine();
            this.AddMedicineCommand = new AddMedicineCommand(this.addMedicine, _ => true);
        }

        private async Task<bool> addMedicine(Medicine medicine)
        {
            return await ((App)App.Current).MedicineClient.CreateMedicineAsync(medicine);
        }
    }
}
