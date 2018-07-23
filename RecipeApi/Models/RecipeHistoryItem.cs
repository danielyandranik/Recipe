namespace RecipeApi.Models
{
    /// <summary>
    /// Describes sold medicine from recipe.
    /// </summary>
    public class RecipeHistoryItem
    {
        /// <summary>
        /// An identifier of the medicine.
        /// </summary>
        public string MedicineId { get; set; }

        /// <summary>
        /// Sold count of the medicine.
        /// </summary>
        public int Count { get; set; }
    }
}
