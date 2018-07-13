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
        [BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		public string Name { get; set; }
        public string Maker { get; set; }
        public string Country { get; set; }
	}
}
