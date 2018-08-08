using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using RecipeApi.Models;
using RecipeApi.Repositories;
using System.Linq;

namespace RecipeApi.Controllers
{
    [Authorize(Policy = "CanWorkWithRecipe")]
    [Produces("application/json")]
    [Route("api/recipes")]
    public class RecipesController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipesController(IRecipeRepository recipeRepository)
        {
            this._recipeRepository = recipeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = this.Request.Query;
            if(query.Count > 0)
            {
                StringValues patientId;
                if (query.TryGetValue("patientId", out patientId))
                {
                    //if(User.FindFirst("current_profile").Value == "Patient")
                    //{
                    //    // Get patientId from user managment
                    //    // check if patiendIds are not equal
                    //    return new UnauthorizedResult();
                    //}
                    return new ObjectResult(await this._recipeRepository.GetAllRecipesByPatient(int.Parse(patientId[0])));
                }
                return new NotFoundResult();
            }
            return new ObjectResult(await this._recipeRepository.GetAllRecipes());
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(string id)
        {
            var recipe = await this._recipeRepository.GetRecipe(id);

            if (recipe == null)
            {
                return new NotFoundResult();
            }                
            return new ObjectResult(recipe);
        }

        [Authorize(Policy = "DoctorProfile")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Recipe recipe)
        {
            foreach(var item in recipe.RecipeItems)
            {
                item.LeftCount = item.Count;
            }
            await this._recipeRepository.Create(recipe);
            return new OkObjectResult(recipe);
        }

        [Authorize(Policy = "CanChangeRecipe")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]Recipe recipe)
        {
            var dbRecipe = await this._recipeRepository.GetRecipe(id);

            if (dbRecipe == null)
            {
                return new NotFoundResult();
            }
                
            recipe.Id = dbRecipe.Id;

            await this._recipeRepository.Update(recipe);
            return new OkObjectResult(recipe);
        }

        [Authorize(Policy = "DoctorProfile")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var dbRecipe = await this._recipeRepository.GetRecipe(id);

            if (dbRecipe == null)
            {
                return new NotFoundResult();
            }

            await this._recipeRepository.Delete(id);
            return new OkResult();
        }
    }
}
