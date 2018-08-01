using GalaSoft.MvvmLight;
using RecipeClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
