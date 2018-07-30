using Desktop.ViewModels;
using System.Windows.Controls;

namespace Desktop.Views.Pages
{
    /// <summary>
    /// Interaction logic for CreateRecipe.xaml
    /// </summary>
    public partial class CreateRecipe : Page
    {
        public CreateRecipe()
        {
            InitializeComponent();
            this.DataContext = new CreateRecipeViewModel();
        }
    }
}
