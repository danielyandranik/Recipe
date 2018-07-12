using MongoDB.Driver;
using RecipeApi.Models;

namespace RecipeApi.Context
{
    public interface IRecipeHistoryContext
    {
        IMongoCollection<RecipeHistory> RecipeHistories { get; }
    }
}
