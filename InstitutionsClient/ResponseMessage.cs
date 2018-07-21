namespace InstitutionClient
{
    public class ResponseMessage<T>
    {
		public int StatusCode { get; set; }

        public bool IsSuccessStatusCode { get; set; }

		public T Content { get; set; }
    }
}