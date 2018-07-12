using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using RecipeApi.Context;
using RecipeApi.Models;

namespace RecipeApi.Repositories
{
    public class RecipeHistoryRepository : IRecipeHistoryRepository
    {
        private readonly IRecipeHistoryContext _context;

        public RecipeHistoryRepository(IRecipeHistoryContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<RecipeHistory>> GetAllRecipes()
        {
            return await this._context.RecipeHistories.Find(_ => true).ToListAsync();
        }

        public Task<RecipeHistory> GetRecipe(int id)
        {
            FilterDefinition<RecipeHistory> filter = Builders<RecipeHistory>.Filter.Eq(recipeHistory => recipeHistory.Id, id);
            return this._context
            .RecipeHistories
            .Find(filter)
            .FirstOrDefaultAsync();
        }

        public async Task Create(RecipeHistory recipeHistory)
        {
            await this._context.RecipeHistories.InsertOneAsync(recipeHistory);
        }

        public async Task<bool> Update(RecipeHistory recipeHistory)
        {
            ReplaceOneResult updateResult = await this._context
            .RecipeHistories
            .ReplaceOneAsync(filter: r => r.Id == recipeHistory.Id, replacement: recipeHistory);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<RecipeHistory> filter = Builders<RecipeHistory>.Filter.Eq(recipeHistory => recipeHistory.Id, id);
            DeleteResult deleteResult = await this._context
            .RecipeHistories
            .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }  
    }
}
