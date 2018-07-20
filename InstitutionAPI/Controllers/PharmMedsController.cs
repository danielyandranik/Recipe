using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InstitutionAPI.Models;
using DatabaseAccess.Repository;

namespace InstitutionAPI.Controllers
{

    [Authorize(Policy = "HasProfile")]
    [Produces("application/json")]
    [Route("api/pharmmeds")]
    public class PharmMedsController : Controller
    {
        /// <summary>
        /// Repository
        /// </summary>
        private DataManager _dataManager;

        /// <summary>
        /// Medicine Client
        /// </summary>
        //private Client _client;

        /// <summary>
        /// Creates new instance of institution controller
        /// </summary>
        public PharmMedsController(DataManager dataManager)
        {
            this._dataManager = dataManager;
            //this._client = client;
        }

        /// <summary>
        /// Get medicine by id
        /// </summary>
        /// <param name="id">Medicine id</param>
        /// <returns></returns> 
        //[HttpGet("{id}")]
        //[Authorize(Policy = "HasProfile")]
        //public async Task<IActionResult> Get(int id)
        //{
        //    var pharmacy = await this._dataManager.OperateAsync<int, Institution>("GetInstitution", id) as Institution;

        //    var pharmMedicines = await this._dataManager.OperateAsync<int, PharmMedicine>("GetPharmacyMedicine", id) as IEnumerable<PharmMedicine>;

        //    var result = new List<Medicine>();

        //    foreach (var pharmMedicine in pharmMedicines)
        //    {
        //        var response = await _client.GetAsync<MedicineInfo>($"api/medicines/{pharmMedicine.Id}");

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var medicineInfo = response.Content as MedicineInfo;
        //            var medicine = new Medicine()
        //            {
        //                MedicineInfo = medicineInfo,
        //                Quantity = pharmMedicine.Quantity,
        //                Price = pharmMedicine.Price,
        //                Pharmacy = pharmacy
        //            };

        //            result.Add(medicine);
        //        }
        //    }

        //    // if no content retun 204
        //    if (result == null)
        //        return new StatusCodeResult(204);

        //    // return JSON serialized content
        //    return new JsonResult(result);
        //}

        [HttpGet("{id}")]
        [Authorize(Policy = "HasProfile")]
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
        [Authorize(Policy = "HasProfile")]
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
        [Authorize(Policy = "HasProfile")]
        //[Authorize(Policy = "PharmacyAdminProfile")]
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
        /// <param name="medicine">The pharmacy medicine info</param>
        /// <returns></returns>
        //[HttpPost]
        //[Authorize(Policy = "HasProfile")]
        //[Authorize(Policy = "PharmacyAdminProfile")]
        //public async Task<IActionResult> Post([FromBody]PharmMedicine pharmMed)
        //{
        //    var dbPharmacy = await this._dataManager.OperateAsync<int, Institution>("GetInstitution", pharmMed.PharmacyId) as Institution;

        //    if (dbPharmacy is null)
        //    {
        //        return new NotFoundResult();
        //    }

        //    var dbMedicine = await _client.GetAsync<MedicineInfo>($"api/medicines/{pharmMed.Id}");

        //    if (dbMedicine is null)
        //    {
        //        return new NotFoundResult();
        //    }

        //    // adding medicine
        //    var addedMeds = await this._dataManager.OperateAsync<PharmMedicine, object>("AddPharmacyMedicine", pharmMed);

        //    // if medicine exists return 'Conflict' code
        //    if ((int)addedMeds == -1)
        //        return new StatusCodeResult(409);

        //    // returning 200
        //    return Ok();
        //}

        [HttpPost]
        [Authorize(Policy = "HasProfile")]
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
        //[Authorize(Policy = "PharmacistProfile")]
        [Authorize(Policy = "HasProfile")]
        [Route("quantity")]
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
        [Authorize(Policy = "HasProfile")]
        //[Authorize(Policy = "PharmacyAdminProfile")]
        [Route("price")]
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
