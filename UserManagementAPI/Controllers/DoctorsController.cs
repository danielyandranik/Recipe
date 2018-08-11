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
        /// Gets doctor by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>action result</returns>
        [HttpGet("{id:int}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            // gettting userId
            var userId = this.GetUserId();

            //if (userId != id)
            //return new StatusCodeResult(401);

            // getting doctor
            var doctor = this._dataManager.Operate<int, Doctor>("GetDoctorById", id);

            // returning result
            if (doctor == null)
                return new StatusCodeResult(404);

            return new JsonResult(doctor);
        }

        [HttpGet("{hospital}")]
        [Authorize]
        public async Task<IActionResult> Get(string hospital)
        {
            var unapprovedDoctors = await this._dataManager.OperateAsync<string, UnapprovedDoctor>("GetUnapprovedDoctors", hospital);

            if (unapprovedDoctors == null)
                return new StatusCodeResult(204);

            return new JsonResult(unapprovedDoctors);
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
            // checking id
            if (doctor.UserId != this.GetUserId())
                return new StatusCodeResult(401);

            // adding new doctor
            var result = (int)this._dataManager.Operate<Doctor, object>("CreateDoctor", doctor);

            // returning result
            return this.GetActionResult(result);
        }

        /// <summary>
        /// Puts doctor
        /// </summary>
        /// <param name="doctorUpdateInfo">Doctor update information</param>
        /// <returns>action result</returns>
        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody]DoctorUpdateInfo doctorUpdateInfo)
        {
            // checking id
            if (doctorUpdateInfo.UserId != this.GetUserId())
                return new StatusCodeResult(401);

            // updating doctor
            var result = (int)this._dataManager.Operate<DoctorUpdateInfo, object>("UpdateDoctor", doctorUpdateInfo);

            // returning result
            return this.GetActionResult(result);
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete()
        {
            // getting user id
            var userId = this.GetUserId();

            // deleting
            var result = (int)this._dataManager.Operate<int, object>("DeleteDoctor", userId);

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
    }
}
