using GalaSoft.MvvmLight;
using System.Threading.Tasks;
using Desktop.Commands;
using System.Collections.ObjectModel;
using InstitutionClient.Models;
using System.Windows;

namespace Desktop.ViewModels
{
    public class HospitalsViewModel : ViewModelBase
    {
        private ObservableCollection<Institution> hospitals;

        private Institution editableHospital;

        private Visibility visibility;

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

        public Institution EditableHospital
        {
            get
            {
                return this.editableHospital;
            }
            set
            {
                this.Set("EditableHospital", ref this.editableHospital, value);
            }
        }

        public Visibility Visibility
        {
            get
            {
                return this.visibility;
            }
            set
            {
                this.Set("Visibility", ref this.visibility, value);
            }
        }

        public DeleteHospitalCommand DeleteHospitalCommand
        {
            get => this.deleteHospitalCommand;
        }

        public EditHospitalCommand EditHospitalCommand
        {
            get => this.editHospitalCommand;
        }

        public HospitalsViewModel()
        {
            this.Visibility = (User.Default.CurrentProfile == "ministry_worker") || (User.Default.CurrentProfile == "hospital_admin") ? Visibility.Visible : Visibility.Collapsed;
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
    }
}