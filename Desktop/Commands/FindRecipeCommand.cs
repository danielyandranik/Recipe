using Desktop.Services;
using Desktop.ViewModels;
using Desktop.Views.Windows;
using RecipeClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Commands
{
    public class FindRecipeCommand : AsyncCommand<string, ResponseMessage<Recipe>>
    {
        private SellMedicinesViewModel ViewModel;

        private RecipeClient.RecipeClient client;

        public FindRecipeCommand(SellMedicinesViewModel viewModel, Func<string, Task<ResponseMessage<Recipe>>> executeMethod, Func<string, bool> canExecuteMethod)
            : base(executeMethod, canExecuteMethod)
        {
            this.ViewModel = viewModel;
            this.client = ((App)App.Current).RecipeClient;
        }

        public async override void Execute(object parameter)
        {
            var response = await this.ExecuteAsync((string)parameter);

            if (!response.IsSuccessStatusCode)
            {
                RecipeMessageBox.Show("Couldn't find recipe");
                return;
            }

            var recipe = response.Content;

            var loadService = new LoadRecipesService();

            this.ViewModel.Recipe = await loadService.Map(recipe);

        }
    }
}
