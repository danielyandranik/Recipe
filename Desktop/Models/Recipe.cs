using System;
using System.Collections.ObjectModel;

namespace Desktop.Models
{
    /// <summary>
    /// Class for recipe
    /// </summary>
    public class Recipe
    {
        /// <summary>
        /// Gets or sets the identifier of the recipe
        /// </summary>
        public string Id { get; set; } = null;

        /// <summary>
        /// Gets or sets the date of creation of the recipe
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets doctor name
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// Gets or sets hospital name
        /// </summary>
        public string HospitalName { get; set; }

        /// <summary>
        /// Gets or sets patient user name.
        /// </summary>
        public string PatientUserName { get; set; }

        /// <summary>
        /// Gets or sets Recipe Items
        /// </summary>
        public ObservableCollection<RecipeItem> RecipeItems { get; set; }

        /// <summary>
        /// Creates new instance of <see cref="Recipe"/>
        /// </summary>
        public Recipe()
        {
            this.RecipeItems = new ObservableCollection<RecipeItem>();
        }
    }
}
