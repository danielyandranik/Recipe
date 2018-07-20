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

namespace Desktop.Views.Pages
{
    /// <summary>
    /// Interaction logic for Medicines.xaml
    /// </summary>
    public partial class Medicines : Page
    {
        public Medicines()
        {
            InitializeComponent();
            this.Tweets.ItemsSource = new[] 
            {
                new Medicine
                {
                    Id = "Id_1",
                    Name = "Ascofen",
                    Maker = "John LLC",
                    Country = "Armenia",
                    Units = "Miligram",
                    ShelfLife = 15,
                    Description = "Empty"
                },
                new Medicine
                {
                    Id = "Id_1",
                    Name = "Ascofen",
                    Maker = "John LLC",
                    Country = "Armenia",
                    Units = "Miligram",
                    ShelfLife = 15,
                    Description = "Empty"
                },
                new Medicine
                {
                    Id = "Id_1",
                    Name = "Ascofen",
                    Maker = "John LLC",
                    Country = "Armenia",
                    Units = "Miligram",
                    ShelfLife = 15,
                    Description = "Empty"
                },
                new Medicine
                {
                    Id = "Id_1",
                    Name = "Ascofen",
                    Maker = "John LLC",
                    Country = "Armenia",
                    Units = "Miligram",
                    ShelfLife = 15,
                    Description = "Empty"
                },
                new Medicine
                {
                    Id = "Id_1",
                    Name = "Ascofen",
                    Maker = "John LLC",
                    Country = "Armenia",
                    Units = "Miligram",
                    ShelfLife = 15,
                    Description = "Empty"
                },
                new Medicine
                {
                    Id = "Id_1",
                    Name = "Ascofen",
                    Maker = "John LLC",
                    Country = "Armenia",
                    Units = "Miligram",
                    ShelfLife = 15,
                    Description = "Empty"
                },
                new Medicine
                {
                    Id = "Id_1",
                    Name = "Ascofen",
                    Maker = "John LLC",
                    Country = "Armenia",
                    Units = "Miligram",
                    ShelfLife = 15,
                    Description = "Empty"
                },
                new Medicine
                {
                    Id = "Id_1",
                    Name = "Ascofen",
                    Maker = "John LLC",
                    Country = "Armenia",
                    Units = "Miligram",
                    ShelfLife = 15,
                    Description = "Empty"
                },
                new Medicine
                {
                    Id = "Id_1",
                    Name = "Ascofen",
                    Maker = "John LLC",
                    Country = "Armenia",
                    Units = "Miligram",
                    ShelfLife = 15,
                    Description = "Empty"
                },
                new Medicine
                {
                    Id = "Id_1",
                    Name = "Ascofen",
                    Maker = "John LLC",
                    Country = "Armenia",
                    Units = "Miligram",
                    ShelfLife = 15,
                    Description = "Empty"
                },
                new Medicine
                {
                    Id = "Id_1",
                    Name = "Ascofen",
                    Maker = "John LLC",
                    Country = "Armenia",
                    Units = "Miligram",
                    ShelfLife = 15,
                    Description = "Empty"
                },
                new Medicine
                {
                    Id = "Id_1",
                    Name = "Ascofen",
                    Maker = "John LLC",
                    Country = "Armenia",
                    Units = "Miligram",
                    ShelfLife = 15,
                    Description = "Empty"
                },
                new Medicine
                {
                    Id = "Id_1",
                    Name = "Ascofen",
                    Maker = "John LLC",
                    Country = "Armenia",
                    Units = "Miligram",
                    ShelfLife = 15,
                    Description = "Empty"
                },
                new Medicine
                {
                    Id = "Id_1",
                    Name = "Ascofen",
                    Maker = "John LLC",
                    Country = "Armenia",
                    Units = "Miligram",
                    ShelfLife = 15,
                    Description = "Empty"
                },
                new Medicine
                {
                    Id = "Id_1",
                    Name = "Ascofen",
                    Maker = "John LLC",
                    Country = "Armenia",
                    Units = "Miligram",
                    ShelfLife = 15,
                    Description = "Empty"
                },
                new Medicine
                {
                    Id = "Id_1",
                    Name = "Ascofen",
                    Maker = "John LLC",
                    Country = "Armenia",
                    Units = "Miligram",
                    ShelfLife = 15,
                    Description = "Empty"
                }
            };
        }

        private void EditMedicineContextMenu_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class Medicine
    {
        /// <summary>
        /// Medicine ID.
        /// </summary>
        public string Id { get; set; } = null;

        /// <summary>
        /// Name of the Medicine.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Who made this product(Company name).
        /// </summary>
        public string Maker { get; set; }

        /// <summary>
        /// Country where it was made.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Units how it counts.
        /// </summary>
        public string Units { get; set; }

        /// <summary>
        /// Shelf life by mothes.
        /// </summary>
        public int ShelfLife { get; set; }

        /// <summary>
        /// Information about Medicine.
        /// </summary>
        public string Description { get; set; }
    }
}
