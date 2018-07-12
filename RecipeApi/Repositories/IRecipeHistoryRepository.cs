using RecipeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApi.Repositories
{
    interface IRecipeHistoryRepository
    {
        Task<IEnumerable<RecipeHistory>> GetAllRecipes();

        Task<RecipeHistory> GetRecipe(int recipeHistoryId);

        Task Create(RecipeHistory recipeHistory);

        Task<bool> Update(RecipeHistory recipeHistory);

        Task<bool> Delete(int recipeHistoryId);
    }
}
