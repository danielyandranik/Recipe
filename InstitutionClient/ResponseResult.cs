namespace InstitutionClient
{
    /// <summary>
    /// Type for Response result
    /// </summary>
    public class ResponseResult
    {
        /// <summary>
        /// The response Content
        /// </summary>
        public object Content { get; set; }

        /// <summary>
        /// The response status
        /// </summary>
        public Status Status { get; set; }
    }
}
