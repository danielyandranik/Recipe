﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InstitutionsAPI.Models;
using DatabaseAccess.Repository;
using Microsoft.Extensions.Primitives;

namespace InstitutionsAPI.Controllers
{
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
        ///  Get istitutions of some category
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = "has_profile")]
        public async Task<IActionResult> Get()
        {
            // the result
            var result = default(object);

            // get parameter
            StringValues param;

            // integer for parsing
            int id;

            // request query
            var query = this.Request.Query;

            // getting the result by given parameter
            if (query.TryGetValue("id", out param))
            {
                int.TryParse(param, out id);
                result = await this._dataManager.OperateAsync<int, Institution>("GetInstitution", id);

            }
            else if (query.TryGetValue("medicineId", out param))
            {
                int.TryParse(param, out id);
                result = await this._dataManager.OperateAsync<int, Institution>("GetPharmaciesByMedicine", id);
            }
            else if (query.Count > 0)
            {
                StringValues type;
                if (query.TryGetValue("type", out type))
                {
                    if (query.TryGetValue("address", out param))
                    {
                        var info = new AddressInfo { Type = type, Address = param };
                        result = await this._dataManager.OperateAsync<AddressInfo, Institution>("GetInstitutionsByAddress", info);
                    }
                    else if (query.TryGetValue("name", out param))
                    {
                        var info = new NameInfo { Type = type, Name = param };
                        result = await this._dataManager.OperateAsync<NameInfo, Institution>("GetInstitutionsByName", info);
                    }
                    else
                    {
                        result = await this._dataManager.OperateAsync<string, Institution>("GetInstitutions", type);
                    }
                }

            }

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
        [Authorize(Policy = "has_privilege")]
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
        [Authorize(Policy = "has_privilege")]
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
