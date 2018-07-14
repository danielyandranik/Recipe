using MedicineAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineAPI.Repositories
{
    public interface IMedicineRepository
    {

        /// <summary>
        /// Getting all medicines.
        /// </summary>
        /// <returns>List of Medicines.</returns>
        Task<IEnumerable<Medicine>> GetAllMedicines();

        /// <summary>
        /// Getting medicine by "ID".
        /// </summary>
        /// <param name="id">Medicine ID.</param>
        /// <returns>Medicine having this ID.</returns>
        Task<Medicine> GetMedicineByID(string id);

        /// <summary>
        /// Getting medicine by "Name".
        /// </summary>
        /// <param name="name">Medicine Name.</param>
        /// <returns>Medicin with specified name.</returns>
        Task<Medicine> GetMedicineByName(string name);

        /// <summary>
        /// Adds a new medicine.
        /// </summary>
        /// <param name="medicine">Medicine to add.</param>
        /// <returns></returns>
        Task Create(Medicine medicine);

        /// <summary>
        /// Makes changes.
        /// </summary>
        /// <param name="medicine">Change the information of the medicine.</param>
        /// <returns>Returns True if the request has been succeeded.</returns>
        Task<bool> Update(Medicine medicine);

        /// <summary>
        /// Delete the medicine.
        /// </summary>
        /// <param name="id">Medicine ID from wich it's going to be deleted.</param>
        /// <returns>Returns True if the request has been succeeded.</returns>
        Task<bool> Delete(string id);
    }
}
