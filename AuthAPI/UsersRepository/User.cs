namespace AuthAPI.UsersRepository
{
    /// <summary>
    /// Class for users
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets Activity
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets Current Profile
        /// </summary>
        public int CurrentProfile { get; set; }
    }
}
