using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using RecipeApi.Models;
using RecipeApi.Repositories;

namespace RecipeApi.Controllers
{
    [Produces("application/json")]
    [Route("api/recipeHistories")]
    public class RecipeHistoriesController : Controller
    {
        private readonly IRecipeHistoryRepository _recipeHistoryRepository;

        public RecipeHistoriesController(IRecipeHistoryRepository recipeHistoryRepository)
        {
            this._recipeHistoryRepository = recipeHistoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = this.Request.Query;
            if (query.Count > 0)
            {
                StringValues recipeId;
                if (query.TryGetValue("recipeId", out recipeId))
                {
                    return new ObjectResult(await this._recipeHistoryRepository.GetRecipeHistoryByRecipe(recipeId[0]));
                }
                return new NotFoundResult();
            }
            return new ObjectResult(await this._recipeHistoryRepository.GetAllRecipeHistories());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var recipe = await this._recipeHistoryRepository.GetRecipeHistory(id);

            if (recipe == null)
            {
                return new NotFoundResult();
            }
            return new ObjectResult(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RecipeHistory recipeHistory)
        {
            await this._recipeHistoryRepository.Create(recipeHistory);
            return new OkObjectResult(recipeHistory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]RecipeHistory recipeHistory)
        {
            var dbRecipeHistory = await this._recipeHistoryRepository.GetRecipeHistory(id);

            if (dbRecipeHistory == null)
            {
                return new NotFoundResult();
            }

            recipeHistory.Id = dbRecipeHistory.Id;

            await this._recipeHistoryRepository.Update(recipeHistory);
            return new OkObjectResult(recipeHistory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var dbRecipeHistory = await this._recipeHistoryRepository.GetRecipeHistory(id);

            if (dbRecipeHistory == null)
            {
                return new NotFoundResult();
            }

            await this._recipeHistoryRepository.Delete(id);
            return new OkResult();
        }
    }
}