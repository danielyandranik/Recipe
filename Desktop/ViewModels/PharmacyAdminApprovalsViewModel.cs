using Desktop.Commands;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.ViewModels
{
    public class PharmacyAdminApprovalsViewModel : ViewModelBase
    {
        private ObservableCollection<UnapprovedPharmacist> unapprovedPharmacists;

        private UserManagementApiClient client;

        public ObservableCollection<UnapprovedPharmacist> UnapprovedPharmacists
        {
            get => this.unapprovedPharmacists;

            set => this.Set("UnapprovedPharmacists", ref this.unapprovedPharmacists, value);
        }

        public AsyncCommand<int, Response<string>> ApprovePharmacistCommand { get; private set; }

        public PharmacyAdminApprovalsViewModel()
        {
            this.ApprovePharmacistCommand = new ApprovePharmacistCommand(this, this.AprrovePharmacist, _ => true);
            this.client = ((App)App.Current).UserApiClient;
        }

        private async Task<Response<string>> AprrovePharmacist(int userId)
        {
            return await client.ApprovePharmacistAsync(userId);
        }
    }
}
