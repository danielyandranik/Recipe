using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Desktop.ViewModels;
using Desktop.Views.Windows;
using RecipeClient;
using UserManagementConsumer.Client;
using System.Linq;

namespace Desktop.Commands
{
    /// <summary>
    /// Add recipe history command
    /// </summary>
    public class AddRecipeHistoryCommand : AsyncCommand<RecipeHistory, ResponseMessage<string>>
    {
        /// <summary>
        /// Sell medicines view model
        /// </summary>
        private SellMedicinesViewModel viewModel;

        /// <summary>
        /// Creates new instance of <see cref="AddRecipeHistoryCommand"/>
        /// </summary>
        /// <param name="sellMedicinesViewModel">Sell medicines view model.</param>
        /// <param name="executeMethod">Execute method.</param>
        /// <param name="canExecuteMethod">CanExecute method.</param>
        public AddRecipeHistoryCommand(SellMedicinesViewModel sellMedicinesViewModel, Func<RecipeHistory, Task<ResponseMessage<string>>> executeMethod, Func<RecipeHistory, bool> canExecuteMethod) :
            base(executeMethod, canExecuteMethod)
        {
            this.viewModel = sellMedicinesViewModel;
        }

        /// <summary>
        /// Executes command operation.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        public async override void Execute(object parameter)
        {
            var historyItems = (IEnumerable<RecipeHistoryItem>)parameter;

            var response = await ((App)App.Current).UserApiClient.GetPharmacistByIdAsync(User.Default.Id);

            if(response.Status == Status.Error)
            {
                RecipeMessageBox.Show("Couldn't get the pharmacy.");
                return;
            }

            var pharmacyId = response.Result.PharmacyId;

            var recipeHistory = new RecipeHistory
            {
                CreatedOn = DateTime.Now,
                RecipeId = this.viewModel.Recipe.Id,
                PharmacyId = pharmacyId,
                Sold = historyItems.ToList<RecipeHistoryItem>()
            };

            await this.ExecuteAsync(recipeHistory);
        }
    }
}
