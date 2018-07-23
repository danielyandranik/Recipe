using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Desktop.Views.Controls
{
    /// <summary>
    /// Interaction logic for Recipe.xaml
    /// </summary>
    public partial class Recipe : UserControl
    {
        public Recipe()
        {
            InitializeComponent();
            this.recipeItems.ItemsSource = new[]
            {
                new RecipeItem
                {
                    MedicineName = "Ascofen",
                    Count = 200,
                    UseFrequencyUnit = "Daily",
                    TimesPerUnit = 3,
                    CountPerUse = 1
                },
                new RecipeItem
                {
                    MedicineName = "Aspirin",
                    Count = 50,
                    UseFrequencyUnit = "Weakly",
                    TimesPerUnit = 1,
                    CountPerUse = 2
                },
                new RecipeItem
                {
                    MedicineName = "Ascofen",
                    Count = 200,
                    UseFrequencyUnit = "Daily",
                    TimesPerUnit = 3,
                    CountPerUse = 1
                },

            };
        }
    }

    public class RecipeItem
    {
        public string MedicineName { get; set; }

        public int Count { get; set; }

        public string UseFrequencyUnit { get; set; }

        public int TimesPerUnit { get; set; }

        public int CountPerUse { get; set; }
    }
}
