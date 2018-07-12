using RecipeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApi.Repositories
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<Recipe>> GetAllRecipes();

        Task<Recipe> GetRecipe(string id);

        Task Create(Recipe recipe);

        Task<bool> Update(Recipe recipe);

        Task<bool> Delete(string id);
    }
}
