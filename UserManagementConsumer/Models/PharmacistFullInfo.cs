namespace UserManagementConsumer.Models
{
    /// <summary>
    /// Pharmacist full information
    /// </summary>
    public class PharmacistFullInfo:Pharmacist
    {
        /// <summary>
        /// Gets or sets user id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets started working time
        /// </summary>
        public string StartedWorking { get; set; }
    }
}
