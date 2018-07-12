using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RecipeApi.Models;

namespace RecipeApi.Context
{
    public class RecipeHistoryContext : IRecipeHistoryContext
    {
        private readonly IMongoDatabase _db;

        public RecipeHistoryContext(IOptions<Settings.Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<RecipeHistory> Recipes => this._db.GetCollection<RecipeHistory>("RecipeHistories");
    }
}
