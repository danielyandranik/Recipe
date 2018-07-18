using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicineAPI.Context;
using MedicineAPI.Models;
using MongoDB.Driver;

namespace MedicineAPI.Repositories
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly IMedicineContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public MedicineRepository(IMedicineContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new medicine.
        /// </summary>
        /// <param name="medicine">Medicine to add.</param>
        /// <returns></returns>
        public async Task Create(Medicine medicine)
        {
            await this._context.Medicines.InsertOneAsync(medicine);
        }


        /// <summary>
        /// Delete the medicine.
        /// </summary>
        /// <param name="id">Medicine ID from wich it's going to be deleted.</param>
        /// <returns>Returns True if the request has been succeeded.</returns>
        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Medicine> filter = Builders<Medicine>.Filter.Eq(medicine => medicine.Id, id);
            DeleteResult deleteResult = await this._context
            .Medicines
            .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;

        }

        /// <summary>
        /// Getting all medicines.
        /// </summary>
        /// <returns>List of Medicines.</returns>
        public async Task<IEnumerable<Medicine>> GetAllMedicines()
        {
            return await this._context.Medicines.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<Medicine>> GetByCountry(string country)
        {
            FilterDefinition<Medicine> filter = Builders<Medicine>.Filter.Eq(medicine => medicine.Country, country);
            return await this._context
            .Medicines
            .Find(filter)
            .ToListAsync<Medicine>();
        }

        /// <summary>
        /// Getting medicine by "ID".
        /// </summary>
        /// <param name="id">Medicine ID.</param>
        /// <returns>Medicine having this ID.</returns>
        public async Task<Medicine> GetMedicineByID(string id)
        {
            FilterDefinition<Medicine> filter = Builders<Medicine>.Filter.Eq(medicine => medicine.Id, id);
            return await this._context
            .Medicines
            .Find(filter)
            .FirstOrDefaultAsync<Medicine>();
        }

        /// <summary>
        /// Getting medicine by "Name".
        /// </summary>
        /// <param name="name">Medicine Name.</param>
        /// <returns>Medicin with specified name.</returns>
        public async Task<Medicine> GetMedicineByName(string name)
        {
            FilterDefinition<Medicine> filter = Builders<Medicine>.Filter.Eq(medicine => medicine.Name, name);
            return await this._context
            .Medicines
            .Find(filter)
            .FirstOrDefaultAsync<Medicine>();
        }

        /// <summary>
        /// Makes changes.
        /// </summary>
        /// <param name="medicine">Change the information of the medicine.</param>
        /// <returns>Returns True if the request has been succeeded.</returns>
        public async Task<bool> Update(Medicine medicine)
        {
            ReplaceOneResult updateResult = await this._context
            .Medicines
            .ReplaceOneAsync(filter: r => r.Id == medicine.Id, replacement: medicine);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;

        }
    }
}
