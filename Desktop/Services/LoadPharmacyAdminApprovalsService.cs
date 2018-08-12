using Desktop.ViewModels;
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
    /// <summary>
    /// Pharmacy admin approvals service
    /// </summary>
    class LoadPharmacyAdminApprovalsService
    {
        /// <summary>
        /// Pharmacy admin approval viewmodel
        /// </summary>
        private readonly PharmacyAdminApprovalsViewModel pharmacyAdminApprovalsViewModel;

        /// <summary>
        /// User management API client
        /// </summary>
        private readonly UserManagementApiClient client;

        /// <summary>
        /// Creates new instance of <see cref="LoadPharmacyAdminApprovalsService"/>
        /// </summary>
        /// <param name="pharmacyAdminApprovalsViewModel">Pharmacy admin approvals viewmodel</param>
        public LoadPharmacyAdminApprovalsService(PharmacyAdminApprovalsViewModel pharmacyAdminApprovalsViewModel)
        {
            this.pharmacyAdminApprovalsViewModel = pharmacyAdminApprovalsViewModel;
            this.client = ((App)App.Current).UserApiClient;
        }

        /// <summary>
        /// Loads approvals
        /// </summary>
        /// <returns>nothing</returns>
        public async Task Load()
        {
            var pharmacyAdminResponse = await this.client.GetPharmacyAdminByIdAsync(User.Default.Id);

            var pharmacyAdmin = pharmacyAdminResponse.Result;

            var unapprovedPharmacistsResponse = await this.client.GetUnapprovedPharmacists(pharmacyAdmin.PharmacyName);

            var unapprovedPharmacists = unapprovedPharmacistsResponse.Result;

            this.pharmacyAdminApprovalsViewModel.UnapprovedPharmacists = new ObservableCollection<UnapprovedPharmacist>(unapprovedPharmacists);
        }
    }
}
