using Desktop.Commands;
using Desktop.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Desktop.ViewModels
{
    public class RecipesViewModel : LoadablePageViewModel
    {
        private ObservableCollection<Recipe> recipes;

        private readonly SendQRCommand _sendQrCommand;

        public ICommand SendQrCommand => this._sendQrCommand;

        public ObservableCollection<Recipe> Recipes
        {
            get => this.recipes;
            set => this.Set("Recipes", ref this.recipes, value);
        }

        public RecipesViewModel()
        {
            this._sendQrCommand = new SendQRCommand(this);
        }
    }
}
