using Desktop.Commands;
using GalaSoft.MvvmLight;
using MedicineApiClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.ViewModels
{
    public class HospitalAdminApprovalViewModel : ViewModelBase
    {
        private ObservableCollection<Profile> waitingCollection;

        private UserManagementApiClient client;

        public ObservableCollection<Profile> WaitingCollection
        {
            get => this.waitingCollection;

            set => this.Set("WaitingCollection", ref this.waitingCollection, value);
        }

        public AsyncCommand<int, Response<string>> ApproveDoctorCommand { get; }

        public HospitalAdminApprovalViewModel()
        {
            this.ApproveDoctorCommand = new AsyncCommand<int, Response<string>>(this.AprroveDoctor, _ => true);
            this.client = ((App)App.Current).UserApiClient;
        }

        private async Task<Response<string>> AprroveDoctor(int profileId)
        {
            return await client.PutDoctorAsync(new DoctorUpdateInfo());
        }
    }
}
