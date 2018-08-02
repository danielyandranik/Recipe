using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;
using Desktop.ViewModels;

namespace Desktop.Services
{
    /// <summary>
    /// Hospital admin approvals service
    /// </summary>
    class LoadHospitalAdminApprovals
    {
        /// <summary>
        /// Hospital admin approval viewmodel
        /// </summary>
        private readonly HospitalAdminApprovalViewModel hospitalAdminApprovalsViewModel;

        /// <summary>
        /// User management API client
        /// </summary>
        private readonly UserManagementApiClient client;

        /// <summary>
        /// Creates new instance of <see cref="LoadHospitalAdminApprovals"/>
        /// </summary>
        /// <param name="hospitalAdminApprovalsViewModel">Hospital Admin approvals viewmodel</param>
        public LoadHospitalAdminApprovals(HospitalAdminApprovalViewModel hospitalAdminApprovalsViewModel)
        {
            this.hospitalAdminApprovalsViewModel = hospitalAdminApprovalsViewModel;
            this.client = ((App)App.Current).UserApiClient;
        }

        /// <summary>
        /// Loads approvals
        /// </summary>
        /// <returns>nothing</returns>
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
