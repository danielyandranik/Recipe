using Desktop.ViewModels;
using Desktop.Views.Windows;
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
    /// Ministry worker approvals service.
    /// </summary>
    class LoadUnapprovedHospitalAdminsService
    {
        /// <summary>
        /// Mininstry worker approvals viewmodel.
        /// </summary>
        private readonly MinistryWorkerApprovalsViewModel viewModel;

        /// <summary>
        /// User management api client.
        /// </summary>
        private readonly UserManagementApiClient client;

        /// <summary>
        /// Creates new instance of <see cref="MinistryWorkerApprovalsViewModel"/>
        /// </summary>
        /// <param name="viewModel">Ministry worker approvals view model</param>
        public LoadUnapprovedHospitalAdminsService(MinistryWorkerApprovalsViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.client = ((App)App.Current).UserApiClient;
        }

        /// <summary>
        /// Loads necessary approvals.
        /// </summary>
        /// <returns></returns>
        public async Task Load()
        {
            var unapprovedHospitalAdminsResponse = await this.client.GetUnapprovedHospitalAdmins();

            if (unapprovedHospitalAdminsResponse.Status == Status.Error)
            {
                RecipeMessageBox.Show("Couldn't get unapproved hospital admins.");
                return;
            }

            this.viewModel.HospitalAdmins = new ObservableCollection<UnapprovedHospitalAdmin>(unapprovedHospitalAdminsResponse.Result);
        }
    }
}
