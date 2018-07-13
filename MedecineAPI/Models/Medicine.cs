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
		public string Name { get; set; }

        /// <summary>
        /// Who made this product(Company name).
        /// </summary>
        public string Maker { get; set; }

        /// <summary>
        /// Country where it was made.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Units how it counts.
        /// </summary>
        public string Units { get; set; }

        /// <summary>
        /// Shelf life by mothes.
        /// </summary>
        public int ShelfLife { get; set;}


        /// <summary>
        /// Information about Medicine.
        /// </summary>
        public string Description { get; set; }


    }
}
