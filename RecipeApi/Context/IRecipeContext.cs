using MongoDB.Driver;
using RecipeApi.Models;

namespace RecipeApi.Context
{   
    /// <summary>
    /// An interface for getting Recipes collection.
    /// </summary>
    public interface IRecipeContext
    {
        /// <summary>
        /// Recipes collection.
        /// </summary>
        IMongoCollection<Recipe> Recipes { get; }
    }
}
