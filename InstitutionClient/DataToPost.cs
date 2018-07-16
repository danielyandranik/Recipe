namespace InstitutionClient
{
    public class DataToPost<T>
    {
        public int Id { get; set; }

        public T Item { get; set; }
    }
}
