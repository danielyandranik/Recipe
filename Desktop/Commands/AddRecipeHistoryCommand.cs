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
            var historyItems = (IEnumerable<RecipeHistoryItem>)this.viewModel.HistoryItems;

            var response = await ((App)App.Current).UserApiClient.GetPharmacistByIdAsync(User.Default.Id);

            var dictionary = App.Current.Resources;

            if(response.Status == Status.Error)
            {
                RecipeMessageBox.Show((string)dictionary["pharmacy_find_fail"]);
                return;
            }

            var pharmacyId = response.Result.PharmacyId;

            var recipeHistory = new RecipeHistory
            {
                CreatedOn = DateTime.Now,
                RecipeId = this.viewModel.Recipe.First().Id,
                PharmacyId = pharmacyId,
                Sold = historyItems.ToList<RecipeHistoryItem>()
            };

            var sellResponse = await this.ExecuteAsync(recipeHistory);

            this.viewModel.Start();
            this.viewModel.Recipe = null;
            this.viewModel.RecipeId = null;

            if (!sellResponse.IsSuccessStatusCode)
            {
                RecipeMessageBox.Show((string)dictionary["sell_medicines_fail"]);
                return;
            }

            RecipeMessageBox.Show((string)dictionary["sell_medicines_success"]);
        }
    }
}
