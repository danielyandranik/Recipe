using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RecipeApi.Models;

namespace RecipeApi.Context
{
    /// <summary>
    /// RecipeHistory context class for getting RecipeHistories.
    /// </summary>
    public class RecipeHistoryContext : IRecipeHistoryContext
    {
        /// <summary>
        /// An instance of IMongDatabase.
        /// </summary>
        private readonly IMongoDatabase _db;

        /// <summary>
        /// Creates a new instance of RecipeHistory context class.
        /// </summary>
        /// <param name="options">IOptions instance.</param>
        public RecipeHistoryContext(IOptions<Settings.Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }

        /// <summary>
        /// RecipeHistories collection.
        /// </summary>
        public IMongoCollection<RecipeHistory> RecipeHistories => this._db.GetCollection<RecipeHistory>("RecipeHistories");
    }
}
