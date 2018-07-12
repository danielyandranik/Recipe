using MongoDB.Driver;
using RecipeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApi.Context
{
    public interface IRecipeContext
    {
        IMongoCollection<Recipe> Recipes { get; }
    }
}
