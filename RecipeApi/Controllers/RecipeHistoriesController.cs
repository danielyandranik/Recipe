using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using RecipeApi.Models;
using RecipeApi.Repositories;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace RecipeApi.Controllers
{
    [Authorize(Policy = "CanWorkWithRecipeHistory")]
    [Produces("application/json")]
    [Route("api/recipeHistories")]
    public class RecipeHistoriesController : Controller
    {
        private readonly IRecipeHistoryRepository _recipeHistoryRepository;
		private readonly IRecipeRepository _recipeRepository;

        public RecipeHistoriesController(IRecipeHistoryRepository recipeHistoryRepository, IRecipeRepository recipeRepository)
        {
			this._recipeRepository = recipeRepository;
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

		[Authorize(Policy = "CanChangeRecipeHistory")]
		[HttpPost]
        public async Task<IActionResult> Post([FromBody]RecipeHistory recipeHistory)
        {
            var recipe = await this._recipeRepository.GetRecipe(recipeHistory.RecipeId);

            if (recipe == null)
            {
                return new NotFoundResult();
            }
          
            
            foreach(var medicine in recipeHistory.Sold)
            {
                var rec = recipe.RecipeItems.Where(med => med.MedicineId == medicine.MedicineId).First();
                if(rec.LeftCount < medicine.Count)
                {
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }
                else
                {
                    rec.LeftCount -= medicine.Count;
                }
                

            }

            var response = await this._recipeRepository.Update(recipe);
            if(!response)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }

            return new OkObjectResult(recipeHistory);
        }

        [Authorize(Policy = "CanChangeRecipeHistory")]
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

        //No one can work with this
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