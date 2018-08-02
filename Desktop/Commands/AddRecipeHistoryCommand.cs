using Desktop.ViewModels;
using RecipeClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Commands
{
    internal class AddRecipeHistoryCommand : AsyncCommand<RecipeHistory, ResponseMessage<string>>
    {
        private SellMedicinesViewModel viewModel;

        public AddRecipeHistoryCommand(SellMedicinesViewModel sellMedicinesViewModel, Func<RecipeHistory, Task<ResponseMessage<string>>> executeMethod, Func<RecipeHistory, bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
            this.viewModel = sellMedicinesViewModel;
        }

        public async override void Execute(object parameter)
        {
            var recipeHistory = new RecipeHistory();

            await this.ExecuteAsync(recipeHistory);
        }
    }
}
