using RecipeApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeApi.Repositories
{
    /// <summary>
    /// Recipe history repository interface.
    /// </summary>
    public interface IRecipeHistoryRepository
    {
        /// <summary>
        /// Gets all recipe histories asinc.
        /// </summary>
        /// <returns>Returns a task whose result is IEnumerable of RecipeHistory instances.</returns>
        Task<IEnumerable<RecipeHistory>> GetAllRecipeHistories();

        /// <summary>
        /// Gets all recipe histories belongs to the specified recipe.
        /// </summary>
        /// <param name="recipeId">The identifier of the recipe.</param>
        /// <returns>Returns a task whose result is IEnumerable of RecipeHistory instances.</returns>
        Task<IEnumerable<RecipeHistory>> GetRecipeHistoryByRecipe(string recipeId);

        /// <summary>
        /// Gets the recipe history by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns a task whose result is Recipe instance.</returns>
        Task<RecipeHistory> GetRecipeHistory(string id);

        /// <summary>
        /// Creates a new recipe history.
        /// </summary>
        /// <param name="recipeHistory">A new recipe history.</param>
        /// <returns>Returns a task.</returns>
        Task Create(RecipeHistory recipeHistory);

        /// <summary>
        /// Updates the recipe history.
        /// </summary>
        /// <param name="recipeHistory">The recipe history.</param>
        /// <returns>Returns a task whose result is true if update was succeed.</returns>
        Task<bool> Update(RecipeHistory recipeHistory);

        /// <summary>
        /// Deletes the recipe history.
        /// </summary>
        /// <param name="id">The id of removable recipe history.</param>
        /// <returns>Returns a task whose result is true if delete was succeed.</returns>
        Task<bool> Delete(string id);
    }
}
