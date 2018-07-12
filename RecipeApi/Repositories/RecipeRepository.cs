using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using RecipeApi.Context;
using RecipeApi.Models;

namespace RecipeApi.Repositories
{
    public class RecipeRepository : IRecipeRepository
    { 
        private readonly IRecipeContext _context;

        public RecipeRepository(IRecipeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipes()
        {
            return await this._context.Recipes.Find(_ => true).ToListAsync();
        }

        public Task<Recipe> GetRecipe(string id)
        {
            FilterDefinition<Recipe> filter = Builders<Recipe>.Filter.Eq(recipe => recipe.Id, id);
            return this._context
            .Recipes
            .Find(filter)
            .FirstOrDefaultAsync();
        }

        public async Task Create(Recipe recipe)
        {
            await this._context.Recipes.InsertOneAsync(recipe);
        }

        public async Task<bool> Update(Recipe recipe)
        {
            ReplaceOneResult updateResult = await this._context
            .Recipes
            .ReplaceOneAsync(filter: r => r.Id == recipe.Id, replacement: recipe);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Recipe> filter = Builders<Recipe>.Filter.Eq(recipe => recipe.Id, id);
            DeleteResult deleteResult = await this._context
            .Recipes
            .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
