﻿using System;
using System.Threading.Tasks;
using UserManagementConsumer.Client;
using Desktop.Views.Windows;

namespace Desktop.Commands
{
    /// <summary>
    /// Approve doctor command
    /// </summary>
    public class ApproveDoctorCommand : AsyncCommand<int, Response<string>>
    {
        /// <summary>
        /// Creates new instance of <see cref="ApproveDoctorCommand"/>
        /// </summary>
        /// <param name="executeMethod">Execute method</param>
        /// <param name="canExecuteMethod">CanExecute method</param>
        public ApproveDoctorCommand(Func<int, Task<Response<string>>> executeMethod, Func<int, bool> canExecuteMethod) : 
            base(executeMethod, canExecuteMethod)
        {

        }

        /// <summary>
        /// Executes doctor approving command
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        public override async void Execute(object parameter)
        {
            var response = await this.ExecuteAsync((int)parameter);
            
            if(response.Status == Status.Error)
            {
                RecipeMessageBox.Show("Error occured");
                return;
            }

            RecipeMessageBox.Show("Profile is successfully approved");
        }
    }
}
