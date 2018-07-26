﻿using System.Threading.Tasks;
using UserManagementConsumer.Client;
using UserManagementConsumer.Models;

namespace Desktop.Services
{
    public class PatientProfileService : ProfileService
    {
        public async override Task<Response<string>> Execute(object parameter)
        {
            return await this.userManagementApiClient.PostPatientAsync((Patient)parameter);
        }
    }
}
