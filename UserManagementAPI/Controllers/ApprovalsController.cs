using DatabaseAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    /// <summary>
    /// Controller for approvals
    /// </summary>
    [Produces("application/json")]
    [Route("api/approvals")]
    public class ApprovalsController : Controller
    {
        /// <summary>
        /// Data manager
        /// </summary>
        private DataManager _dataManager;

        /// <summary>
        /// Creates new instance of Approvals controller
        /// </summary>
        /// <param name="dataManager">Data manager</param>
        public ApprovalsController(DataManager dataManager)
        {
            // setting fields
            this._dataManager = dataManager;
        }


        /// <summary>
        /// Puts doctor profile approval
        /// </summary>
        /// <param name="approval">approval</param>
        /// <returns>action result</returns>
        [HttpPut]
        [Authorize(Policy = "IsHospitalDirector")]
        [Route("doctors")]
        public IActionResult PutDoctorApproval([FromBody]Approval approval)
        {
            if (approval.Type != "doctor")
                return new StatusCodeResult(400);

            // getting operation result
            var result = (int)this._dataManager.Operate<Approval,object>("ApproveProfile", approval);

            // returning result
            return this.GetActionResult(result);
        }

        /// <summary>
        /// Puts pharmacist profile approval
        /// </summary>
        /// <param name="approval">approval</param>
        /// <returns>action result</returns>
        [HttpPut]
        [Authorize(Policy = "IsPharmacyAdmin")]
        [Route("pharmacists")]
        public IActionResult PutPharmacistApproval([FromBody]Approval approval)
        {
            if (approval.Type != "pharmacist")
                return new StatusCodeResult(400);

            // getting operation result
            var result = (int)this._dataManager.Operate<Approval, object>("ApproveProfile", approval);

            // returning result
            return this.GetActionResult(result);
        }

        /// <summary>
        /// Gets action result
        /// </summary>
        /// <param name="result">result</param>
        /// <returns>action result</returns>
        public IActionResult GetActionResult(int result)
        {
            return result == 0 ? new StatusCodeResult(409) : new StatusCodeResult(200);
        }
    }
}