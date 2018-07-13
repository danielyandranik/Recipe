using RecipeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApi.Repositories
{
    public interface IRecipeHistoryRepository
    {
        Task<IEnumerable<RecipeHistory>> GetAllRecipeHistories();

        Task<IEnumerable<RecipeHistory>> GetRecipeHistoryByRecipe(string recipeId);

        Task<RecipeHistory> GetRecipeHistory(string id);

        Task Create(RecipeHistory recipeHistory);

        Task<bool> Update(RecipeHistory recipeHistory);

        Task<bool> Delete(string id);
    }
}
