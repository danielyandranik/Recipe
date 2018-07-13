using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using RecipeApi.Context;
using RecipeApi.Models;

namespace RecipeApi.Repositories
{
    /// <summary>
    /// Describes a RecipeHistory repository.
    /// </summary>
    public class RecipeHistoryRepository : IRecipeHistoryRepository
    {
        /// <summary>
        /// An IRecipeHistoryContext field.
        /// </summary>
        private readonly IRecipeHistoryContext _context;

        /// <summary>
        /// Creates a new RecipeHistoryRepository instacne.
        /// </summary>
        /// <param name="context">IRecipeHistoryContext instance.</param>
        public RecipeHistoryRepository(IRecipeHistoryContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Gets all recipe histories asinc.
        /// </summary>
        /// <returns>Returns a task whose result is IEnumerable of RecipeHistory instances.</returns>
        public async Task<IEnumerable<RecipeHistory>> GetAllRecipeHistories()
        {
            return await this._context.RecipeHistories.Find(_ => true).ToListAsync();
        }

        /// <summary>
        /// Gets the recipe history by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns a task whose result is Recipe instance.</returns>
        public Task<RecipeHistory> GetRecipeHistory(string id)
        {
            FilterDefinition<RecipeHistory> filter = Builders<RecipeHistory>.Filter.Eq(recipeHistory => recipeHistory.Id, id);
            return this._context
            .RecipeHistories
            .Find(filter)
            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets all recipe histories belongs to the specified recipe.
        /// </summary>
        /// <param name="recipeId">The identifier of the recipe.</param>
        /// <returns>Returns a task whose result is IEnumerable of RecipeHistory instances.</returns>
        public async Task<IEnumerable<RecipeHistory>> GetRecipeHistoryByRecipe(string recipeId)
        {
            FilterDefinition<RecipeHistory> filter = Builders<RecipeHistory>.Filter.Eq(recipeHistory => recipeHistory.RecipeId, recipeId);
            return await this._context
            .RecipeHistories
            .Find(filter)
            .ToListAsync<RecipeHistory>();
        }

        /// <summary>
        /// Creates a new recipe history.
        /// </summary>
        /// <param name="recipeHistory">A new recipe history.</param>
        /// <returns>Returns a task.</returns>
        public async Task Create(RecipeHistory recipeHistory)
        {
            await this._context.RecipeHistories.InsertOneAsync(recipeHistory);
        }

        /// <summary>
        /// Updates the recipe history.
        /// </summary>
        /// <param name="recipeHistory">The recipe history.</param>
        /// <returns>Returns a task whose result is true if update was succeed.</returns>
        public async Task<bool> Update(RecipeHistory recipeHistory)
        {
            ReplaceOneResult updateResult = await this._context
            .RecipeHistories
            .ReplaceOneAsync(filter: r => r.Id == recipeHistory.Id, replacement: recipeHistory);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        /// <summary>
        /// Deletes the recipe history.
        /// </summary>
        /// <param name="id">The id of removable recipe history.</param>
        /// <returns>Returns a task whose result is true if delete was succeed.</returns>
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
