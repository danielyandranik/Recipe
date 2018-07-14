using System.Threading.Tasks;
using DatabaseAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    /// <summary>
    /// Doctors controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/doctors")]
    public class DoctorsController : Controller
    {
        /// <summary>
        /// Data manager
        /// </summary>
        private DataManager _dataManager;

        /// <summary>
        /// Creates new instance of Doctors controller
        /// </summary>
        /// <param name="dataManager">Data manager</param>
        public DoctorsController(DataManager dataManager)
        {
            // setting fields
            this._dataManager = dataManager;
        }

        /// <summary>
        /// Gets all doctors public information
        /// </summary>
        /// <returns>action result</returns>
        [HttpGet]
        [Authorize(Policy = "HasProfile")]
        public async Task<IActionResult> Get()
        {
            // getting doctors
            var result = await this._dataManager.OperateAsync<DoctorPublicInfo>("GetAllDoctors");

            // if there are no doctors return 404
            if (result == null)
                return new StatusCodeResult(404);

            // return 200
            return new JsonResult(result);
        }

        /// <summary>
        /// Posts new doctor
        /// </summary>
        /// <param name="doctor">doctor</param>
        /// <returns>action result</returns>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]Doctor doctor)
        {
            // adding new doctor
            var result = (int)this._dataManager.Operate<Doctor, object>("CreateDoctor", doctor);

            // if no doctor is added return Bad request code
            if (result == 0)
                return new StatusCodeResult(400);

            // return Success code
            return new StatusCodeResult(200);
        }
    }
}
