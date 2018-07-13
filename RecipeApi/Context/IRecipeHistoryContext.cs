using MongoDB.Driver;
using RecipeApi.Models;

namespace RecipeApi.Context
{
    /// <summary>
    /// An interface for getting RecipeHistories collection.
    /// </summary>
    public interface IRecipeHistoryContext
    {
        /// <summary>
        /// RecipeHistories collection.
        /// </summary>
        IMongoCollection<RecipeHistory> RecipeHistories { get; }
    }
}
