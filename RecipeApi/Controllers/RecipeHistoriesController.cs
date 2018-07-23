using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using RecipeApi.Models;
using RecipeApi.Repositories;

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
			var recipeId = recipeHistory.RecipeId;

			var history = await this._recipeHistoryRepository.GetRecipeHistoryByRecipe(recipeId);

			Dictionary<string, int> sold = new Dictionary<string, int>();
			
			foreach (var medicine in recipeHistory.Sold)
			{
				if (sold.ContainsKey(medicine.MedicineId))
				{
					sold.TryGetValue(medicine.MedicineId, out var count);
					sold[medicine.MedicineId] = count + medicine.Count;
				}
				else if (!sold.TryAdd(medicine.MedicineId, medicine.Count))
					throw new System.Exception("Dictionary problem");
			}

			foreach (var historyIteam in history)
			{
				foreach (var medicine in historyIteam.Sold)
				{
					if (sold.ContainsKey(medicine.MedicineId))
					{
						sold.TryGetValue(medicine.MedicineId, out var count);
						sold[medicine.MedicineId] = count + medicine.Count;
					}
				}
			}

			var recipe = await this._recipeRepository.GetRecipe(recipeHistory.RecipeId);

			if (recipe == null)
			{
				return new NotFoundResult();
			}

			Dictionary<string, int> recipeMedicines = new Dictionary<string, int>();

			foreach (var medicine in recipe.RecipeItems)
			{
				if (recipeMedicines.ContainsKey(medicine.MedicineId))
				{
					recipeMedicines.TryGetValue(medicine.MedicineId, out var count);
					recipeMedicines[medicine.MedicineId] = count + medicine.Count;
				}
				else recipeMedicines.Add(medicine.MedicineId, medicine.Count);
			}

			 

			foreach (var medicine in sold)
			{
				if (medicine.Value > recipeMedicines[medicine.Key])
				{
					// change RecipeHistory.Sold list to dict 
					// change recipeHistory[medicine.Key] = -1
					// retuen count = -1 in this cell
				}
			}

			await this._recipeHistoryRepository.Create(recipeHistory);
            return new OkObjectResult(recipeHistory);
        }

		//[Authorize(Policy = "CanChangeRecipeHistory")]
		//[HttpPut("{id}")]
  //      public async Task<IActionResult> Put(string id, [FromBody]RecipeHistory recipeHistory)
  //      {
  //          var dbRecipeHistory = await this._recipeHistoryRepository.GetRecipeHistory(id);

  //          if (dbRecipeHistory == null)
  //          {
  //              return new NotFoundResult();
  //          }

  //          recipeHistory.Id = dbRecipeHistory.Id;

  //          await this._recipeHistoryRepository.Update(recipeHistory);
  //          return new OkObjectResult(recipeHistory);
  //      }

		//No one can work with this 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    var dbRecipeHistory = await this._recipeHistoryRepository.GetRecipeHistory(id);

        //    if (dbRecipeHistory == null)
        //    {
        //        return new NotFoundResult();
        //    }

        //    await this._recipeHistoryRepository.Delete(id);
        //    return new OkResult();
        //}
    }
}