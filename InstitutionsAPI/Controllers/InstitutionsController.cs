using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InstitutionsAPI.Models;
using DatabaseAccess.Repository;
using Microsoft.Extensions.Primitives;

namespace InstitutionsAPI.Controllers
{
    [Authorize(Policy = "has_profile")]
    [Produces("application/json")]
    [Route("api/institutions")]
    public class InstitutionsController : Controller
    {
        /// <summary>
        /// Repository
        /// </summary>
        private DataManager _dataManager;

        /// <summary>
        /// Creates new instance of institution controller
        /// </summary>
        public InstitutionsController(DataManager dataManager)
        {
            this._dataManager = dataManager;
        }

        /// <summary>
        /// Gets enumerable of certain type of institutions
        /// </summary>
        /// <param name="type">Institution type</param>
        /// <returns></returns>
        [HttpGet("{type}")]
        public async Task<IActionResult> Get(string type)
        {
            // getting result
            var result = await this._dataManager.OperateAsync<string, Institution>("GetInstitutions", type);

            // if no content retun 204
            if (result == null)
                return new StatusCodeResult(204);

            // return JSON serialized content
            return new JsonResult(result);
        }

        /// <summary>
        /// Get institution by id
        /// </summary>
        /// <param name="id">Institution id</param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            // getting result
            var result = await this._dataManager.OperateAsync<int, Institution>("GetInstitution", id);

            // if no content retun 204
            if (result == null)
                return new StatusCodeResult(204);

            // return JSON serialized content
            return new JsonResult(result);
        }

        /// <summary>
        /// Get pharmacies by medicine id
        /// </summary>
        /// <param name="id">Medicine id</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("pharmacy")]
        public async Task<IActionResult> GetPharmasy(int id)
        {
            // getting result
            var result = await this._dataManager.OperateAsync<int, Institution>("GetPharmaciesByMedicine", id);

            // if no content retun 204
            if (result == null)
                return new StatusCodeResult(204);

            // return JSON serialized content
            return new JsonResult(result);
        }

        [HttpGet]
        [Authorize(Policy = "has_profile")]
        public async Task<IActionResult> GetPharmasy([FromBody] NameInfo info)
        {
            // getting result
            var result = await this._dataManager.OperateAsync<NameInfo, Institution>("GetPharmaciesByMedicine", info);

            // if no content retun 204
            if (result == null)
                return new StatusCodeResult(204);

            // return JSON serialized content
            return new JsonResult(result);
        }

        [HttpGet]
        [Authorize(Policy = "has_profile")]
        public async Task<IActionResult> GetPharmasy([FromBody] AddressInfo info)
        {
            // getting result
            var result = await this._dataManager.OperateAsync<AddressInfo, Institution>("GetPharmaciesByMedicine", info);

            // if no content retun 204
            if (result == null)
                return new StatusCodeResult(204);

            // return JSON serialized content
            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = this.Request.Query;
            if (query.Count > 0)
            {
                StringValues type;
                if (query.TryGetValue("type", out type))
                {
                    StringValues address;
                    if (query.TryGetValue("address", out address))
                    {

                    }
                    return new ObjectResult(await this._recipeRepository.GetAllRecipesByPatient(int.Parse(patientId[0])));
                }
                return new NotFoundResult();
            }
            return new ObjectResult(await this._recipeRepository.GetAllRecipes());
        }


        /// <summary>
        /// Add institution
        /// </summary>
        /// <param name="user">Institution</param>
        /// <returns>Action result</returns>
        [HttpPost]
        [Authorize(Policy = "ministry_worker")]
        public async Task<IActionResult> Post([FromBody]Institution institution)
        {
            // adding institution
            var addedInstitutions = await this._dataManager.OperateAsync<Institution, object>("AddInstitution", institution);

            // if institution exists return 'Conflict' code
            if ((int)addedInstitutions < 1)
                return new StatusCodeResult(409);

            // returning 200
            return Ok();
        }

        /// <summary>
        ///  Update institution
        /// </summary>
        /// <param name="id">Intitution id</param>
        /// <param name="institution">Updating info</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Policy = "institution_admin")]
        public async Task<IActionResult> Put([FromBody]Institution institution)
        {
            // check if the institution exists
            var dbInstitution = await this._dataManager.OperateAsync<int, Institution>("GetInstitution", institution.Id);

            if (dbInstitution is null)
            {
                return new NotFoundResult();
            }

            //getting result
            var result = await this._dataManager.OperateAsync<Institution, object>("UpdateInstitution", institution);

            //returning 200
            return Ok();
        }

        /// <summary>
        /// Delete institution
        /// </summary>
        /// <param name="id">Intitution id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "institution_admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedInstitutions = await this._dataManager.OperateAsync<int, object>("RemoveInstitution", id);

            // if deleting impossible return 'Conflict' code
            if ((int)deletedInstitutions == -1)
                return new StatusCodeResult(409);

            // returning 200
            return Ok();
        }
    }
}
