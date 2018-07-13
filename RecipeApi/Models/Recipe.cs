using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace RecipeApi.Models
{
    public class Recipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public int DoctorId { get; set; }      

        public bool IsApproved { get; set; }

        public int ChiefDoctorId { get; set; } 

        public int PatientId { get; set; }

        public List<RecipeItem> RecipeItems { get; set; }
    }
}
