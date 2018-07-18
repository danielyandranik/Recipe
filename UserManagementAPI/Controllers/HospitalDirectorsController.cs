﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DatabaseAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    /// <summary>
    /// Controller for hospotal directors
    /// </summary>
    [Produces("application/json")]
    [Route("api/hospitaldirectors")]
    public class HospitalDirectorsController : Controller
    {
        /// <summary>
        /// Data manager
        /// </summary>
        private DataManager _dataManager;

        /// <summary>
        /// Creates new instance of HospitalDirectors controller
        /// </summary>
        /// <param name="dataManager">Data manager</param>
        public HospitalDirectorsController(DataManager dataManager)
        {
            // setting fields
            this._dataManager = dataManager;
        }

        /// <summary>
        /// Gets all hospital directors
        /// </summary>
        /// <returns>action result</returns>
        [HttpGet]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Get()
        {
            // getting result
            var result = await this._dataManager.OperateAsync<HospitalDirector>("GetAllHospitalDirectors");

            if (result == null)
                return new StatusCodeResult(204);

            return new JsonResult(result);
        }

        /// <summary>
        /// Gets hospital director by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>action result</returns>
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            if (this.GetUserId() != id)
                return new StatusCodeResult(403);

            // getting result
            var result = this._dataManager.Operate<int, HospitalDirector>("GetHospitalDirectorById", id);

            // returning result
            return new JsonResult(result);
        }

        /// <summary>
        /// Posts new hospital director
        /// </summary>
        /// <param name="hospitalDirector">Hospital director</param>
        /// <returns>action result</returns>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]HospitalDirector hospitalDirector)
        {
            // getting operation result
            var result = (int)this._dataManager
                .Operate<int, HospitalDirector, object>("CreateHospitalDirector", this.GetUserId(), hospitalDirector);

            // returning result
            return this.GetActionResult(result);
        }

        /// <summary>
        /// Puts hospital director
        /// </summary>
        /// <param name="hospitalDirector">Hospital director</param>
        /// <returns>action result</returns>
        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody]HospitalDirector hospitalDirector)
        {
            // getting operation result
            var result = (int)this._dataManager
                .Operate<int, HospitalDirector, object>("UpdateHospitalDirector", this.GetUserId(), hospitalDirector);

            // returnig action result
            return this.GetActionResult(result);
        }

        /// <summary>
        /// Deletes hospital director
        /// </summary>
        /// <returns>action result</returns>
        [HttpDelete]
        [Authorize]
        public IActionResult Delete()
        {
            // getting operation result
            var result = (int)this._dataManager
                .Operate<int, object>("DeleteHospitalDirector", this.GetUserId());

            // returning action result
            return this.GetActionResult(result);
        }

        /// <summary>
        /// Gets user id.
        /// </summary>
        /// <returns>User id.</returns>
        private int GetUserId()
        {
            // returning id
            return int.Parse(
                ((ClaimsIdentity)this.User.Identity).Claims
                .Where(claim => claim.Type == "user_id").First().Value);
        }

        /// <summary>
        /// Gets action result
        /// </summary>
        /// <param name="result">result</param>
        /// <returns>action result</returns>
        private IActionResult GetActionResult(int result)
        {
            if (result == 0)
                return new StatusCodeResult(404);

            return new StatusCodeResult(200);
        }
    }
}
