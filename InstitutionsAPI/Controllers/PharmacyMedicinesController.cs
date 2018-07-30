using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InstitutionsAPI.Models;
using DatabaseAccess.Repository;

namespace InstitutionsAPI.Controllers
{
    [Authorize(Policy = "has_profile")]
    [Produces("application/json")]
    [Route("api/pharmmeds")]
    public class PharmacyMedicinesController : Controller
    {
        /// <summary>
        /// Repository
        /// </summary>
        private DataManager _dataManager;

        /// <summary>
        /// Creates new instance of institution controller
        /// </summary>
        public PharmacyMedicinesController(DataManager dataManager)
        {
            this._dataManager = dataManager;
        }

        /// <summary>
        /// Get medicine by id
        /// </summary>
        /// <param name="id">Medicine id</param>
        /// <returns></returns> 
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await this._dataManager.OperateAsync<int, PharmMedicine>("GetPharmacyMedicine", id);

            // if no content retun 204
            if (result == null)
                return new StatusCodeResult(204);

            // return JSON serialized content
            return new JsonResult(result);
        }

        /// <summary>
        /// Get medicines by pharmacy id
        /// </summary>
        /// <param name="id">Pharmacy id</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("medicine")]
        public async Task<IActionResult> GetMedicine(int id)
        {
            var result = await this._dataManager.OperateAsync<int, PharmMedicine>("GetPharmacyMedicines", id);

            // if no content retun 204
            if (result == null)
                return new StatusCodeResult(204);

            // return JSON serialized content
            return new JsonResult(result);
        }

        /// <summary>
        /// Delete pharmacy medicine by specified id
        /// </summary>
        /// <param name="id">Pharm medicine id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "pharmacy_admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedMeds = await this._dataManager.OperateAsync<int, object>("RemovePharmacyMedicine", id);

            // if deleting impossible return 'Conflict' code
            if ((int)deletedMeds == -1)
                return new StatusCodeResult(409);

            // returning 200
            return Ok();
        }

        /// <summary>
        /// Add pharmacy medicine
        /// </summary>
        /// <param name="pharmMed"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = "pharmacy_admin")]
        public async Task<IActionResult> Post([FromBody]PharmMedicine pharmMed)
        {
            // adding medicine
            var addedMeds = await this._dataManager.OperateAsync<PharmMedicine, object>("AddPharmacyMedicine", pharmMed);

            // if medicine exists return 'Conflict' code
            if ((int)addedMeds == -1)
                return new StatusCodeResult(409);

            // returning 200
            return Ok();
        }

        /// <summary>
        /// Update medicine quantity
        /// </summary>
        /// <param name="medicine">Updated info</param>
        /// <returns></returns>
        [HttpPut]
        [Route("quantity")]
        [Authorize(Policy = "pharmacist")]
        public async Task<IActionResult> Put([FromBody]MedicineQuantityInfo medicine)
        {
            var result = await this._dataManager.OperateAsync<int, object>("GetPharmacyMedicine", medicine.Id);

            if (result is null)
            {
                return new NotFoundResult();
            }

            //getting result
            var updated = await this._dataManager.OperateAsync<MedicineQuantityInfo, object>("UpdateQuantity", medicine);

            //returning 200
            return Ok();
        }

        /// <summary>
        /// Update medicine price
        /// </summary>
        /// <param name="medicine">Updated info</param>
        /// <returns></returns>
        [HttpPut]
        [Route("price")]
        [Authorize(Policy = "pharmacist")]
        public async Task<IActionResult> Put([FromBody]MedicinePriceInfo medicine)
        {
            var result = await this._dataManager.OperateAsync<int, object>("GetPharmacyMedicine", medicine.Id);

            if (result is null)
            {
                return new NotFoundResult();
            }

            //getting result
            var updated = await this._dataManager.OperateAsync<MedicinePriceInfo, object>("UpdatePrice", medicine);

            //returning 200
            return Ok();
        }
    }
}
