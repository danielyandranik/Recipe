using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RecipeApi.Models;

namespace RecipeApi.Context
{
    public class RecipeContext : IRecipeContext
    {
        private readonly IMongoDatabase _db;

        public RecipeContext(IOptions<Settings.Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<Recipe> Recipes => _db.GetCollection<Recipe>("Recipes");
    }
}
