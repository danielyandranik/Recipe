using System.Threading.Tasks;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using Desktop.Commands;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.ViewModels
{
    public class HospitalAdminApprovalViewModel : ViewModelBase
    {
        private ObservableCollection<UnapprovedDoctor> waitingCollection;

        private UserManagementApiClient client;

        public ObservableCollection<UnapprovedDoctor> WaitingCollection
        {
            get => this.waitingCollection;

            set => this.Set("WaitingCollection", ref this.waitingCollection, value);
        }

        public AsyncCommand<int, Response<string>> ApproveDoctorCommand { get; }

        public HospitalAdminApprovalViewModel()
        {
            this.ApproveDoctorCommand = new ApproveDoctorCommand(this, this.AprroveDoctor, _ => true);
            this.client = ((App)App.Current).UserApiClient;
        }

        private async Task<Response<string>> AprroveDoctor(int userId)
        {
            return await client.ApproveDoctorAsync(userId);
        }
    }
}
