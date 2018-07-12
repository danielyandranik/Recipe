using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeApi.Models;
using RecipeApi.Repositories;

namespace RecipeApi.Controllers
{
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Recipe recipe)
        {
            await this._recipeRepository.Create(recipe);
            return new OkObjectResult(recipe);
        }

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
