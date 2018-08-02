using Desktop.Commands;
using Desktop.Models;
using GalaSoft.MvvmLight;
using RecipeClient;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Desktop.ViewModels
{
    internal class CreateRecipeViewModel : ViewModelBase
    {
        private Models.Recipe recipe;

        private Models.RecipeItem addingItem;

        public Models.Recipe Recipe
        {
            get => this.recipe;
            set => this.Set("Recipe", ref this.recipe, value);
        }

        public Models.RecipeItem AddingItem
        {
            get => this.addingItem;
            set => this.Set("AddingItem", ref this.addingItem, value);
        }

        public CreateRecipeCommand CreateRecipeCommand { get; }

        public CreateRecipeViewModel()
        {
            this.Recipe = new Models.Recipe();
            this.CreateRecipeCommand = new CreateRecipeCommand(this.createRecipe, _ => true);
        }

        private async Task<ResponseMessage<string>> createRecipe(RecipeClient.Recipe recipe)
        {
            return await ((App)App.Current).RecipeClient.CreateAsync<RecipeClient.Recipe>(recipe);
        }
    }
}
