using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;
using Desktop.ViewModels;
using Desktop.Models;
using System.Collections.Generic;

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
            var hospitalAdminResponse = await this.client.GetHospitalDirectorByIdAsync(User.Default.Id);

            var hospitalAdmin = hospitalAdminResponse.Result;

            var unapprovedDoctorsResponse = await this.client.GetUnapprovedDoctors(hospitalAdmin.HospitalName);

            var unapprovedDoctors = unapprovedDoctorsResponse.Result;

            this.hospitalAdminApprovalsViewModel.WaitingCollection = new ObservableCollection<UnapprovedDoctor>(unapprovedDoctors);
        }
    }
}