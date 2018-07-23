namespace Desktop.Models
{
    /// <summary>
    /// Class for patients
    /// </summary>
    public class PatientInfo
    {
        /// <summary>
        /// Gets or sets user id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets regional doctor name
        /// </summary>
        public string RegionalDoctorName { get; set; }

        /// <summary>
        /// Gets or sets occupation
        /// </summary>
        public string Occupation { get; set; }

        /// <summary>
        /// Gets or sets Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets Is Alcholic
        /// </summary>
        public bool IsAlcoholic { get; set; }

        /// <summary>
        /// Gets or sets Is drug addicted
        /// </summary>
        public bool IsDrugAddicted { get; set; }
    }
}
