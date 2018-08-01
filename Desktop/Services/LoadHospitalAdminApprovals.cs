using Desktop.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementConsumer.Client;

namespace Desktop.Services
{
    class LoadHospitalAdminApprovals
    {
        private readonly HospitalAdminApprovals hospitalAdminApprovals;

        private readonly UserManagementApiClient client;

        public LoadHospitalAdminApprovals(HospitalAdminApprovals hospitalAdminApprovals)
        {
            this.hospitalAdminApprovals = hospitalAdminApprovals;
            this.client = ((App)App.Current).UserApiClient;
        }

        public async Task Load()
        {
            //
        }
    }
}
