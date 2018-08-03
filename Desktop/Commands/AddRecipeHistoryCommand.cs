using System;
using System.Threading.Tasks;
using Desktop.ViewModels;
using RecipeClient;

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
            var recipeHistory = new RecipeHistory();

            await this.ExecuteAsync(recipeHistory);
        }
    }
}
