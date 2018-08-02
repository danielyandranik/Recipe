using Desktop.Models;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace Desktop.ViewModels
{
    public class RecipesViewModel : ViewModelBase
    {
        private ObservableCollection<Recipe> recipes;

        public ObservableCollection<Recipe> Recipes
        {
            get => this.recipes;
            set => this.Set("Recipes", ref this.recipes, value);
        }
    }
}
