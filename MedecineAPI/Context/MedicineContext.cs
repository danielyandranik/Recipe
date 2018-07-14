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
	public class MedicineContext : IMedicineContext
	{
		private readonly IMongoDatabase _db;
				
		public MedicineContext(IOptions<Settings> options)
		{
			var client = new MongoClient(options.Value.ConnectionString);
			_db = client.GetDatabase(options.Value.Database);
		}

        public IMongoCollection<Medicine> Medicines => _db.GetCollection<Medicine>("Medicines");
    }
}
