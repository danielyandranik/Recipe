using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace RecipeApi.Models
{
    /// <summary>
    /// Describes Recipe model.
    /// </summary>
    public class Recipe
    {
        /// <summary>
        /// An identifier of the recipe.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// The date of creation of the recipe.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// An identifier of the doctor that creates the recipe.
        /// </summary>
        public int DoctorId { get; set; }      

        /// <summary>
        /// An identifier of the patient for whom the recipe is created.
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// A collection of RecipeItem instances.
        /// </summary>
        //public List<RecipeItem> RecipeItems { get; set; }
        public List<RecipeItem> RecipeItems { get; set; }
    }
}
