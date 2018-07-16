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
        /// <summary>
        /// Medicine ID.
        /// </summary>
		[BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

        /// <summary>
        /// Name of the Medicine.
        /// </summary>
        [BsonElement("name")]
		public string Name { get; set; }

        /// <summary>
        /// Who made this product(Company name).
        /// </summary>
        [BsonElement("maker")]
        public string Maker { get; set; }

        /// <summary>
        /// Country where it was made.
        /// </summary>
        [BsonElement("country")]
        public string Country { get; set; }

        /// <summary>
        /// Units how it counts.
        /// </summary>
        [BsonElement("units")]
        public string Units { get; set; }

        /// <summary>
        /// Shelf life by mothes.
        /// </summary>
        [BsonElement("shelfLife")]
        public int ShelfLife { get; set;}

        /// <summary>
        /// Information about Medicine.
        /// </summary>
        [BsonElement("description")]
        public string Description { get; set; }


    }
}
