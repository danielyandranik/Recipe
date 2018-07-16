using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedecineAPI.Settings;
using MedicineAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MedicineAPI.Context
{
    /// <summary>
    /// Medicine context class for getting Medicines collection.
    /// </summary>
	public class MedicineContext : IMedicineContext
	{

        /// <summary>
        /// An instance of IMongDatabase.
        /// </summary>
		private readonly IMongoDatabase _db;

        /// <summary>
        /// Creates a new instance of medicine context class.
        /// </summary>
        /// <param name="options"></param>
        public MedicineContext(IOptions<Settings> options)
		{
			var client = new MongoClient(options.Value.ConnectionString);
			_db = client.GetDatabase(options.Value.Database);
		}

        /// <summary>
        /// Medicine collections.
        /// </summary>
        public IMongoCollection<Medicine> Medicines => _db.GetCollection<Medicine>("Medicines");
    }
}
