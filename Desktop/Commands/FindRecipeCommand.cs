using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using RecipeClient;
using Desktop.Services;
using Desktop.ViewModels;
using Desktop.Views.Windows;

namespace Desktop.Commands
{
    /// <summary>
    /// Find recipe command
    /// </summary>
    public class FindRecipeCommand : AsyncCommand<string, ResponseMessage<RecipeClient.Recipe>>
    {
        /// <summary>
        /// Boolean value indicating the availabality of find button
        /// </summary>
        private bool _isFindAvailable;

        /// <summary>
        /// Sell medicines page view model
        /// </summary>
        private readonly SellMedicinesViewModel _viewModel;

        /// <summary>
        /// Creates new instance of <see cref="FindRecipeCommand"/>
        /// </summary>
        /// <param name="viewModel">Viewmodel</param>
        /// <param name="executeMethod">Execute method</param>
        /// <param name="canExecuteMethod">CanExecute method</param>
        public FindRecipeCommand(SellMedicinesViewModel viewModel, Func<string, Task<ResponseMessage<RecipeClient.Recipe>>> executeMethod, Func<string, bool> canExecuteMethod)
            : base(executeMethod, canExecuteMethod)
        {
            this._viewModel = viewModel;
            this._isFindAvailable = true;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public async override void Execute(object parameter)
        {
            if (!this._isFindAvailable)
                return;

            this._isFindAvailable = false;
            this._viewModel.SetVisibilities(Visibility.Visible, true);

            var dictionary = App.Current.Resources;

            try
            {
                var response = await this.ExecuteAsync((string)parameter);

                if (!response.IsSuccessStatusCode)
                {
                    RecipeMessageBox.Show((string)dictionary["recipe_find_fail"]);
                    return;
                }

                var recipe = response.Content;

                var loadService = new LoadRecipesService();

                this._viewModel.Recipe = new ObservableCollection<Models.Recipe>();
                this._viewModel.Recipe.Add(await loadService.Map(recipe));

                this._viewModel.HistoryItems = this.Map(this._viewModel.Recipe.First());
            }
            catch
            {
                RecipeMessageBox.Show((string)dictionary["recipe_find_fail"]);
            }
            finally
            {
                this._isFindAvailable = true;
                this._viewModel.SetVisibilities(Visibility.Collapsed, false);
            }
        }

        /// <summary>
        /// Maps recipe to recipe history item
        /// </summary>
        /// <param name="recipe">recipe</param>
        /// <returns>recipe history item</returns>
        private ObservableCollection<RecipeHistoryItem> Map(Models.Recipe recipe)
        {
            var historyItems = new ObservableCollection<RecipeHistoryItem>();

            foreach (var item in recipe.RecipeItems)
            {
                historyItems.Add(new RecipeHistoryItem { MedicineId = item.Medicine.Id });
            }

            return historyItems;
        }
    }
}
