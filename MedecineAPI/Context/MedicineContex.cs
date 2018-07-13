using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicineAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MedicineAPI.Context
{
	public class MedicineContex : IMedicineConext
	{
		private readonly IMongoDatabase _db;
				
		public MedicineContex(IOptions<Settings.Settings> options)
		{
			var client = new MongoClient(options.Value.ConnectionString);
			_db = client.GetDatabase(options.Value.Database);
		}

		IMongoCollection<Medicine> IMedicineConext.Medicines => _db.GetCollection<Medicine>("Medicine");
	}
}
