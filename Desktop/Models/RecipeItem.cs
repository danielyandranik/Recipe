using MedicineApiClient;

namespace Desktop.Models
{
    /// <summary>
    /// Class for recipe item.
    /// </summary>
    public class RecipeItem
    {
        /// <summary>
        /// Gets or sets medicine
        /// </summary>
        public Medicine Medicine { get; set; }

        /// <summary>
        /// Gets or sets the count of units
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the unit of frequency
        /// </summary>
        public string UseFrequencyUnit { get; set; }

        /// <summary>
        /// Gets or sets the use frequency per frequency unit
        /// </summary>
        public int TimesPerUnit { get; set; }

        /// <summary>
        /// Gets or sets the count of units per use
        /// </summary>
        public int CountPerUse { get; set; }

        /// <summary>
        /// Left count of this medicine.
        /// </summary>
        public int LeftCount { get; set; }
    }
}
