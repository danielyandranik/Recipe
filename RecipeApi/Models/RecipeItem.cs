namespace RecipeApi.Models
{
    public class RecipeItem
    {
        public int MedicineId { get; set; }

        public int Count { get; set; }

        public string FrequencyUnit { get; set; }

        public int UseFrequencyPerFrequencyUnit { get; set; }

        public int UnitCountPerUse { get; set; }


    }
}