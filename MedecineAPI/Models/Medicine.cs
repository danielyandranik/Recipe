using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MedicineAPI.Models
{
    public class Medicine
    {
		[BsonId]
		public ObjectId Id { get; set; }
		public string Name { get; set; }
		public string Developer { get; set; }
		public string Publisher { get; set; }
		public List<string> Platforms { get; set; }
	}
}
