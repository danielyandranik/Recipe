using Desktop.ViewModels;
using Desktop.Views.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Services
{
    class LoadHospitalAdminApprovals
    {
        private readonly HospitalAdminApprovalViewModel hospitalAdminApprovalsViewModel;

        private readonly UserManagementApiClient client;

        public LoadHospitalAdminApprovals(HospitalAdminApprovalViewModel hospitalAdminApprovalsViewModel)
        {
            this.hospitalAdminApprovalsViewModel = hospitalAdminApprovalsViewModel;
            this.client = ((App)App.Current).UserApiClient;
        }

        public async Task Load()
        {
            var response = await this.client.GetUnapprovedProfilesByTypeAsync("doctor");

            if (response.Status == Status.Error)
            {
                throw new Exception();
            }

            this.hospitalAdminApprovalsViewModel.WaitingCollection = new ObservableCollection<Profile>(response.Result);
        }
    }
}
