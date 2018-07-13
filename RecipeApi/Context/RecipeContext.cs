using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RecipeApi.Models;

namespace RecipeApi.Context
{
    /// <summary>
    /// Recipe context class for getting Recipes collection.
    /// </summary>
    public class RecipeContext : IRecipeContext
    {
        /// <summary>
        /// An instance of IMongDatabase.
        /// </summary>
        private readonly IMongoDatabase _db;

        /// <summary>
        /// Creates a new instance of recipe context class.
        /// </summary>
        /// <param name="options">IOptions instance.</param>
        public RecipeContext(IOptions<Settings.Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            this._db = client.GetDatabase(options.Value.Database);
        }

        /// <summary>
        /// Recipes collections.
        /// </summary>
        public IMongoCollection<Recipe> Recipes => this._db.GetCollection<Recipe>("Recipes");
    }
}
