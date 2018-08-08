namespace RecipeClient
{
    /// <summary>
    /// Describes a recipe item.
    /// </summary>
    public class RecipeItem
    {
        /// <summary>
        /// An identifier of the medicine.
        /// </summary>
        public string MedicineId { get; set; }

        /// <summary>
        /// A count of units.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// A unit of frequency.
        /// </summary>
        public string UseFrequencyUnit { get; set; }

        /// <summary>
        /// A Use frequency per frequency unit.
        /// </summary>
        public int TimesPerUnit { get; set; }

        /// <summary>
        /// A count of units per use.
        /// </summary>
        public int CountPerUse { get; set; }
        
        /// <summary>
        /// Left count of this medicine.
        /// </summary>
        public int LeftCount { get; set; }
    }
}