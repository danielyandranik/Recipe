using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DatabaseAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    /// <summary>
    /// Patients controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/patients")]
    public class PatientsController : Controller
    {
        /// <summary>
        /// Data manager
        /// </summary>
        private DataManager _dataManager;

        /// <summary>
        /// Creates new instance of Patients controller
        /// </summary>
        /// <param name="dataManager">Data manager</param>
        public PatientsController(DataManager dataManager)
        {
            // setting fields
            this._dataManager = dataManager;
        }

        /// <summary>
        /// Gets all results
        /// </summary>
        /// <returns>action result</returns>
        [HttpGet]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> Get()
        {
            // getting patients
            var result = await this._dataManager.OperateAsync<Patient>("GetAllPatients");

            // returning result
            return this.GetActionResult(result);
        }

        /// <summary>
        /// Gets patient by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>action result</returns>
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            // getting result
            var result = this._dataManager.Operate<Patient>("GetPatienById");

            // returning result
            return this.GetActionResult(result);
        }

        /// <summary>
        /// Posts new patient 
        /// </summary>
        /// <param name="patient">patient</param>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]Patient patient)
        {
            // checking id
            if (patient.UserId != this.GetUserId())
                return new StatusCodeResult(401);

            // creating new patient
            var result = (int)this._dataManager.Operate<Patient, object>("CreatePatient", patient);

            // returning result
            return this.GetActionResult(result);
        }

        /// <summary>
        /// Updates patient
        /// </summary>
        /// <param name="patient">patient</param>
        /// <returns>action result</returns>
        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody]Patient patient)
        {
            // checking id
            if (patient.UserId != this.GetUserId())
                return new StatusCodeResult(401);

            // creating new patient
            var result = (int)this._dataManager.Operate<Patient, object>("UpdatePatient", patient);

            // returning result
            return this.GetActionResult(result);
        }

        /// <summary>
        /// Deletes patient
        /// </summary>
        /// <returns>action result</returns>
        [HttpDelete]
        [Authorize]
        public IActionResult Delete()
        {
            // getting user id
            var userId = this.GetUserId();

            // getting user id
            var result = (int)this._dataManager.Operate<int, object>("DeletePatient", userId);

            // returning result
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
        /// <returns>return result</returns>
        private IActionResult GetActionResult(int result)
        {
            if (result == 0 || result == -1)
                return new StatusCodeResult(400);

            return new StatusCodeResult(200);
        }

        /// <summary>
        /// Gets action result
        /// </summary>
        /// <param name="result">result</param>
        /// <returns>action result</returns>
        private IActionResult GetActionResult(object result)
        {
            // if result is null return 404
            if (result == null)
                return new StatusCodeResult(404);

            // returning result
            return new JsonResult(result);
        }
    }
}