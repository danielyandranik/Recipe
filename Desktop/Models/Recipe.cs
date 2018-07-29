using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Desktop.Models
{
    internal class Recipe
    {
        /// <summary>
        /// An identifier of the recipe.
        /// </summary>
        public string Id { get; set; } = null;

        /// <summary>
        /// The date of creation of the recipe.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// An identifier of the doctor that creates the recipe.
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// Checks whether the recipe is approved by chief.
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// An identifier of the chief doctor.
        /// </summary>
        public int HospitalName { get; set; }

        /// <summary>
        /// An identifier of the patient for whom the recipe is created.
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// A collection of RecipeItem instances.
        /// </summary>
        public ObservableCollection<RecipeItem> RecipeItems { get; set; }
    }
}
