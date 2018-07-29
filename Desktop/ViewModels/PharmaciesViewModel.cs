using GalaSoft.MvvmLight;
using System.Threading.Tasks;
using Desktop.Commands;
using System.Collections.ObjectModel;
using System.Diagnostics;
using InstitutionClient.Models;

namespace Desktop.ViewModels
{
    public class PharmaciesViewModel : ViewModelBase
    {
        private ObservableCollection<Institution> pharmacies;

        private Institution editablePharmacy;

        private bool isVisible;

        private DeletePharmacyCommand deletePharmacyCommand;

        private EditPharmacyCommand editPharmacyCommand;

        public ObservableCollection<Institution> Pharmacies
        {
            get
            {
                return this.pharmacies;
            }
            set
            {
                this.Set("Pharmacies", ref this.pharmacies, value);
            }
        }

        public Institution EditablePharmacy
        {
            get
            {
                return this.editablePharmacy;
            }
            set
            {
                this.Set("EditablePharmacy", ref this.editablePharmacy, value);
            }
        }

        public bool IsVisible
        {
            get
            {
                return this.isVisible;
            }
            set
            {
                this.Set("IsVisible", ref this.isVisible, value);
            }
        }

        public PharmaciesViewModel()
        {
            this.LoadPharmacies();

            this.isVisible = (User.Default.CurrentProfile == "ministry_worker") || 
                                    (User.Default.CurrentProfile == "pharmacy_admin") ? true : false;

            this.deletePharmacyCommand = new DeletePharmacyCommand(this.pharmacies, this.deletePharmacy, _ => true);

            this.editPharmacyCommand = new EditPharmacyCommand(this.pharmacies, this.editPharmacy, _ => true);
        }

        private async Task<bool> deletePharmacy(int id)
        {
            return await ((App)App.Current).InstitutionClient.DeleteInstitutionAsync(id);
        }

        private async Task<bool> editPharmacy(Institution pharmacy)
        {
            return await ((App)App.Current).InstitutionClient.UpdateInstitutionAsync(pharmacy);
        }

        private void LoadPharmacies()
        {
            var response = ((App)App.Current).InstitutionClient.GetAllPharmaciesAsync().Result;
            if (!response.IsSuccessStatusCode)
            {
                Debug.Write(response.StatusCode);
                return;
            }

            this.Pharmacies = new ObservableCollection<Institution>(response.Content);
        }
    }
}
