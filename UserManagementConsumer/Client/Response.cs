namespace UserManagementConsumer.Client
{
    /// <summary>
    /// Class for response
    /// </summary>
    /// <typeparam name="T">Type of result</typeparam>
    public class Response<T>
    {
        /// <summary>
        /// Gets or sets message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets result
        /// </summary>
        public T Result { get; set; }
    }
}
