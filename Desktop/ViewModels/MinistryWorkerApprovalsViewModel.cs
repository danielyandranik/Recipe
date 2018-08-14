using Desktop.Commands;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.ViewModels
{
    public class MinistryWorkerApprovalsViewModel : ViewModelBase
    {
        private readonly UserManagementApiClient client;

        private ObservableCollection<UnapprovedHospitalAdmin> hospitalAdmins;

        private ObservableCollection<UnapprovedPharmacyAdmin> pharmacyAdmins;

        public ObservableCollection<UnapprovedHospitalAdmin> HospitalAdmins
        {
            get => this.hospitalAdmins;

            set => this.Set("HospitalAdmins", ref this.hospitalAdmins, value);
        }

        public ObservableCollection<UnapprovedPharmacyAdmin> PharmacyAdmins
        {
            get => this.pharmacyAdmins;

            set => this.Set("PharmacyAdmins", ref this.pharmacyAdmins, value);
        }

        public ApproveHospitalAdminCommand ApproveHospitalAdminCommand { get; private set; }

        public ApprovePharmacyAdminCommand ApprovePharmacyAdminCommand { get; private set; }

        public MinistryWorkerApprovalsViewModel()
        {
            this.client = ((App)App.Current).UserApiClient;
            this.ApproveHospitalAdminCommand = new ApproveHospitalAdminCommand(this, this.ApproveHospitalAdmin, _ => true);
            this.ApprovePharmacyAdminCommand = new ApprovePharmacyAdminCommand(this, this.ApprovePharmacyAdmin, _ => true);
        }

        private async Task<Response<string>> ApproveHospitalAdmin(int id)
        {
            return await this.client.ApproveHospitalAdminAsync(id);
        }

        private async Task<Response<string>> ApprovePharmacyAdmin(int id)
        {
            return await this.client.ApprovePharmacyAdminAsync(id);
        }
    }
}
