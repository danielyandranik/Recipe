using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAccess;
using DatabaseAccess.SpExecuters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InstitutionAPI.Models;
using DatabaseAccess.Repository;

namespace InstitutionAPI.Controllers
{
    //[Authorize(Policy = "")]
    [Produces("application/json")]
    [Route("api/Institutions")]
    public class InstitutionsController : Controller
    {
        /// <summary>
        /// Repository
        /// </summary>
        private DataManager _dataManager;

        /// <summary>
        /// Repository
        /// </summary>
        private Repo<Institution> _repo;

        /// <summary>
        /// Creates new instance of institution controller
        /// </summary>
        public InstitutionsController(DataManager dataManager)
        {
            this._dataManager = dataManager;
        }

        /// <summary>
        /// Stored procedure executer
        /// </summary>
        private readonly SpExecuter spExecuter;

        /// <summary>
        /// Creates new instance of institution controller
        /// </summary>
        public InstitutionsController()
        {
            this.spExecuter = new SpExecuter("(local)", "InstitutionDB", true);
        }

        /// <summary>
        /// Gets enumerable of institutions
        /// </summary>
        /// <returns>enumerable of institutions</returns>
        [HttpGet]
        //[Authorize(Policy = "HasProfile")]
        public async Task<IActionResult> Get()
        {
            // getting result
            var result = await this._dataManager
                .OperateAsync<Institution>("GetInstitutions");

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
        [HttpGet("{id}")]
        [Authorize(Policy = "HasProfile")]
        public async Task<IActionResult> Get(int id)
        {
            // getting result
            var result = await this._dataManager
                .OperateAsync<Institution>("GetInstitution",
                                            new[]
                                            {
                                                new KeyValuePair<string, object>("Id", id)
                                            });

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
        [HttpGet("{id}")]
        [Authorize(Policy = "HasProfile")]
        public async Task<IActionResult> GetPharmacy(int medicineId)
        {
            // getting result
            var result = await this._dataManager.OperateAsync<int, Institution>("GetPharmaciesByMedicine", medicineId);

            // if no content retun 204
            if (result == null)
                return new StatusCodeResult(204);

            // return JSON serialized content
            return new JsonResult(result);
        }

        /// <summary>
        /// Add institution
        /// </summary>
        /// <param name="user">Institution</param>
        /// <returns>Action result</returns>
        [HttpPost]
        [Authorize(Policy = "HighLevel")]
        public async Task<IActionResult> Post([FromBody]Institution institution)
        {
            // adding institution
            var addedInstitutions = await this._dataManager.OperateAsync<Institution, object>("AddInstitution", institution);

            // if institution exists return 'Conflict' code
            if ((int)addedInstitutions == -1)
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
        [HttpPut("{id}")]
        [Authorize(Policy = "CanUpdateInstitution")]
        public async Task<IActionResult> Put(int id, [FromBody]Institution institution)
        {
            var dbInstitution = await this._dataManager.OperateAsync<int, Institution>("GetInstitution", id);

            if ((int)dbInstitution == -1)
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
        [Authorize(Policy = "HighLevel")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedInstitutions  = await this._dataManager.OperateAsync<int, Object>("DeleteInstitution", id);
            
            // if deleting impossible return 'Conflict' code
            if ((int)deletedInstitutions == -1)
                return new StatusCodeResult(409);

            // returning 200
            return Ok();
        }
    }
}



