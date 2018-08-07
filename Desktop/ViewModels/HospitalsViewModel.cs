using GalaSoft.MvvmLight;
using System.Threading.Tasks;
using Desktop.Commands;
using System.Collections.ObjectModel;
using InstitutionClient.Models;
using System.Windows;
using Desktop.Services;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Desktop.ViewModels
{
    public class HospitalsViewModel : ViewModelBase
    {
        private ObservableCollection<Institution> hospitals;

        private Institution editableHospital;

        private Visibility visibility;

        private readonly FilterService<Institution> filterService;

        public IEnumerable<Institution> data;

        private readonly DeleteHospitalCommand deleteHospitalCommand;

        private readonly EditHospitalCommand editHospitalCommand;

        private readonly LoadHospitalsService _loadHospitalsService;

        private readonly LoadCommand _loadCommand;

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

        public ICommand DeleteHospitalCommand => this.deleteHospitalCommand;

        public ICommand EditHospitalCommand => this.editHospitalCommand;

        public ICommand LoadCommand => this._loadCommand;

        public HospitalsViewModel()
        {
            this.Visibility = (User.Default.CurrentProfile == "ministry_worker") || (User.Default.CurrentProfile == "hospital_admin") ? Visibility.Visible : Visibility.Collapsed;
            this.deleteHospitalCommand = new DeleteHospitalCommand(this.hospitals, this.DeleteHospital, _ => true);
            this.editHospitalCommand = new EditHospitalCommand(this.hospitals, this.EditHospital, _ => true);
            this.filterService = new FilterService<Institution>();
            this._loadHospitalsService = new LoadHospitalsService(this);
            this._loadCommand = new LoadCommand(this._loadHospitalsService);
        }

        public async Task Filter(Func<Institution,bool> predicate)
        {
            var hospitals = await this.filterService.FilterAsync(this.data, predicate);

            this.Hospitals = new ObservableCollection<Institution>(hospitals);
        }

        private async Task<bool> DeleteHospital(int id)
        {
            return await ((App)App.Current).InstitutionClient.DeleteInstitutionAsync(id);
        }

        private async Task<bool> EditHospital(Institution hospital)
        {
            return await ((App)App.Current).InstitutionClient.UpdateInstitutionAsync(hospital);
        }
    }
}