using RecipeApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeApi.Repositories
{
    /// <summary>
    /// Recipe repository interface.
    /// </summary>
    public interface IRecipeRepository
    {
        /// <summary>
        /// Gets all recipes.
        /// </summary>
        /// <returns>Returns a task whose result is IEnumerable of Recipe instances.</returns>
        Task<IEnumerable<Recipe>> GetAllRecipes();

        /// <summary>
        /// Gets all recipes belongs to the specified user.
        /// </summary>
        /// <param name="patientId">The patient id.</param>
        /// <returns>Returns a task whose result is IEnumerable of Recipe instances.</returns>
        Task<IEnumerable<Recipe>> GetAllRecipesByPatient(int patientId);

        /// <summary>
        /// Gets the recipe by the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>returns a task whose result is Recip instance.</returns>
        Task<Recipe> GetRecipe(string id);

        /// <summary>
        /// Cretes a new recipe.
        /// </summary>
        /// <param name="recipe">The new Recipe.</param>
        /// <returns>Returns a task.</returns>
        Task Create(Recipe recipe);

        /// <summary>
        /// Updates the recipe.
        /// </summary>
        /// <param name="recipe">The recipe.</param>
        /// <returns>Returns a task whose result is true if update was succeed.</returns>
        Task<bool> Update(Recipe recipe);

        /// <summary>
        /// Removes the recipe by the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Returns a task whose result is true if delete was succeed.</returns>
        Task<bool> Delete(string id);
    }
}
