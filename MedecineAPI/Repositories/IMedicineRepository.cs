using MedicineAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineAPI.Repositories
{
    public interface IMedicineRepository
    {
        Task<IEnumerable<Medicine>> GetAllMedicines();

        Task<Medicine> GetMedicineByID(string id);

        Task<Medicine> GetMedicineByName(string name);

        Task Create(Medicine medicine);

        Task<bool> Update(Medicine medicine);

        Task<bool> Delete(string id);
    }
}
