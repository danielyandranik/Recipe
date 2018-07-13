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

        public MedicineRepository(IMedicineContext context)
        {
            _context = context;
        }


        public async Task Create(Medicine medicine)
        {
            await this._context.Medicines.InsertOneAsync(medicine);
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Medicine> filter = Builders<Medicine>.Filter.Eq(medicine => medicine.Id, id);
            DeleteResult deleteResult = await this._context
            .Medicines
            .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;

        }

        public async Task<IEnumerable<Medicine>> GetAllMedicines()
        {
            return await this._context.Medicines.Find(_ => true).ToListAsync();
        }

        public async Task<Medicine> GetMedicineByID(string id)
        {
            FilterDefinition<Medicine> filter = Builders<Medicine>.Filter.Eq(medicine => medicine.Id, id);
            return await this._context
            .Medicines
            .Find(filter)
            .FirstOrDefaultAsync<Medicine>();
        }

        public async Task<Medicine> GetMedicineByName(string name)
        {
            FilterDefinition<Medicine> filter = Builders<Medicine>.Filter.Eq(medicine => medicine.Name, name);
            return await this._context
            .Medicines
            .Find(filter)
            .FirstOrDefaultAsync<Medicine>();
        }

        public async Task<bool> Update(Medicine medicine)
        {
            ReplaceOneResult updateResult = await this._context
            .Medicines
            .ReplaceOneAsync(filter: r => r.Id == medicine.Id, replacement: medicine);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;

        }
    }
}
