using Desktop.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Desktop.Views.Pages
{
    /// <summary>
    /// Interaction logic for Recipes.xaml
    /// </summary>
    public partial class Recipes : Page
    {
        public readonly RecipesViewModel ViewModel;

        public Recipes()
        {
            InitializeComponent();
            this.ViewModel = new RecipesViewModel();
            this.DataContext = this.ViewModel;
        }
    }
}
