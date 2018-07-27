namespace RecipeApi.Models
{
    /// <summary>
    /// Describes a recipe item.
    /// </summary>
    public class RecipeItem
    {
        /// <summary>
        /// An identifier of the medicine.
        /// </summary>
        //public string MedicineId { get; set; }

        /// <summary>
        /// A count of units.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// A unit of frequency.
        /// </summary>
        public string FrequencyUnit { get; set; }

        /// <summary>
        /// A Use frequency per frequency unit.
        /// </summary>
        public int UseFrequencyPerFrequencyUnit { get; set; }

        /// <summary>
        /// A count of units per use.
        /// </summary>
        public int UnitCountPerUse { get; set; }
    }
}