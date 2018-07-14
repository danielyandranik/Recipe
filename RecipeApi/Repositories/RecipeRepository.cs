using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using RecipeApi.Context;
using RecipeApi.Models;

namespace RecipeApi.Repositories
{
    /// <summary>
    /// Describes Recipe repository.
    /// </summary>
    public class RecipeRepository : IRecipeRepository
    { 
        /// <summary>
        /// IRecipeContext instance.
        /// </summary>
        private readonly IRecipeContext _context;

        /// <summary>
        /// Creates a new instance of Recipe repository.
        /// </summary>
        /// <param name="context">The context.</param>
        public RecipeRepository(IRecipeContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all recipes.
        /// </summary>
        /// <returns>Returns a task whose result is IEnumerable of Recipe instances.</returns>
        public async Task<IEnumerable<Recipe>> GetAllRecipes()
        {
            return await this._context.Recipes.Find(_ => true).ToListAsync();
        }

        /// <summary>
        /// Gets all recipes belongs to the specified user.
        /// </summary>
        /// <param name="patientId">The patient id.</param>
        /// <returns>Returns a task whose result is IEnumerable of Recipe instances.</returns>
        public Task<Recipe> GetRecipe(string id)
        {
            FilterDefinition<Recipe> filter = Builders<Recipe>.Filter.Eq(recipe => recipe.Id, id);
            return this._context
            .Recipes
            .Find(filter)
            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets the recipe by the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>returns a task whose result is Recip instance.</returns>
        public async Task Create(Recipe recipe)
        {
            await this._context.Recipes.InsertOneAsync(recipe);
        }

        /// <summary>
        /// Cretes a new recipe.
        /// </summary>
        /// <param name="recipe">The new Recipe.</param>
        /// <returns>Returns a task.</returns>
        public async Task<bool> Update(Recipe recipe)
        {
            ReplaceOneResult updateResult = await this._context
            .Recipes
            .ReplaceOneAsync(filter: r => r.Id == recipe.Id, replacement: recipe);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        /// <summary>
        /// Updates the recipe.
        /// </summary>
        /// <param name="recipe">The recipe.</param>
        /// <returns>Returns a task whose result is true if update was succeed.</returns>
        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Recipe> filter = Builders<Recipe>.Filter.Eq(recipe => recipe.Id, id);
            DeleteResult deleteResult = await this._context
            .Recipes
            .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        /// <summary>
        /// Removes the recipe by the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns a task whose result is true if delete was succeed.</returns>
        public async Task<IEnumerable<Recipe>> GetAllRecipesByPatient(int patientId)
        {
            FilterDefinition<Recipe> filter = Builders<Recipe>.Filter.Eq(recipe => recipe.PatientId, patientId);
            return await this._context
            .Recipes
            .Find(filter)
            .ToListAsync<Recipe>();
        }
    }
}
