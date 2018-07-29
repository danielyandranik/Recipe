using GalaSoft.MvvmLight;
using System.Threading.Tasks;
using Desktop.Commands;
using System.Collections.ObjectModel;
using System.Diagnostics;
using InstitutionClient.Models;

namespace Desktop.ViewModels
{
    public class HospitalsViewModel : ViewModelBase
    {
        private ObservableCollection<Institution> hospitals;

        private Institution editableHospital;

        private bool isVisible;

        private DeleteHospitalCommand deleteHospitalCommand;

        private EditHospitalCommand editHospitalCommand;

        public ObservableCollection<Institution> Hospitals
        {
            get
            {
                return this.hospitals;
            }
            set
            {
                this.Set("Hospitals", ref this.hospitals, value);
            }
        }

        public Institution EditablePharmacy
        {
            get
            {
                return this.editableHospital;
            }
            set
            {
                this.Set("EditableHospitals", ref this.editableHospital, value);
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

        public HospitalsViewModel()
        {
            this.LoadHospitals();

            this.isVisible = (User.Default.CurrentProfile == "ministry_worker") ||
                                    (User.Default.CurrentProfile == "hospital_admin") ? true : false;

            this.deleteHospitalCommand = new DeleteHospitalCommand(this.hospitals, this.deleteHospital, _ => true);

            this.editHospitalCommand = new EditHospitalCommand(this.hospitals, this.editHospital, _ => true);
        }

        private async Task<bool> deleteHospital(int id)
        {
            return await ((App)App.Current).InstitutionClient.DeleteInstitutionAsync(id);
        }

        private async Task<bool> editHospital(Institution hospital)
        {
            return await ((App)App.Current).InstitutionClient.UpdateInstitutionAsync(hospital);
        }

        private void LoadHospitals()
        {
            var response = ((App)App.Current).InstitutionClient.GetAllHospitalsAsync().Result;
            if (!response.IsSuccessStatusCode)
            {
                Debug.Write(response.StatusCode);
                return;
            }

            this.Hospitals = new ObservableCollection<Institution>(response.Content);
        }
    }
}
