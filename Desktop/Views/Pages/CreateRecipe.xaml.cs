using Desktop.ViewModels;
using System.Windows.Controls;

namespace Desktop.Views.Pages
{
    /// <summary>
    /// Interaction logic for CreateRecipe.xaml
    /// </summary>
    public partial class CreateRecipe : Page
    {
        private readonly CreateRecipeViewModel ViewModel;

        public CreateRecipe()
        {
            InitializeComponent();
            this.ViewModel = new CreateRecipeViewModel();
            this.DataContext = this.ViewModel;
        }

        private void AddItemButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ViewModel.Recipe.RecipeItems.Add(this.ViewModel.AddingItem);
            this.ViewModel.AddingItem = new Models.RecipeItem();
        }
    }
}
