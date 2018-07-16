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
using MedicineApiClient;
using Newtonsoft.Json;

namespace InstitutionAPI.Controllers
{
    public class PharmMedicineController : Controller
    {
        /// <summary>
        /// Repository
        /// </summary>
        private DataManager _dataManager;

        /// <summary>
        /// Medicine Client
        /// </summary>
        private MedicineApiClient.Client _client;

        /// <summary>
        /// Creates new instance of institution controller
        /// </summary>
        public PharmMedicineController(DataManager dataManager, Client client)
        {
            this._dataManager = dataManager;
            this._client = client;
        }

        /// <summary>
        /// Stored procedure executer
        /// </summary>
        private readonly SpExecuter spExecuter;

        /// <summary>
        /// Creates new instance of institution controller
        /// </summary>
        public PharmMedicineController()
        {
            this.spExecuter = new SpExecuter("(local)", "InstitutionDB", true);
        }

        /// <summary>
        /// Get medicines by pharmacy id
        /// </summary>
        /// <param name="id">Pharmacy id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Policy = "HasProfile")]
        public async Task<IActionResult> Get(int pharmacyId)
        {
            var pharmacy = await this._dataManager.OperateAsync<int, Institution>("GetInstitution", pharmacyId);

            var dbMedicines = await this._dataManager.OperateAsync<int, PharmMedicine>("GetPharmacyMedicine", pharmacyId);

            var pharmMedicines = dbMedicines as IEnumerable<PharmMedicine>;

            var result = new List<PharmMedicine>();

            foreach (var pharmMedicine in pharmMedicines)
            {
                var response = await _client.GetMedicineAsync($"api/medicines/{pharmMedicine.Id}");

                if (response.IsSuccessStatusCode)
                {
                    var medicine = response.Result as Medicine;
                    var medInfo = new MedicineInfo()
                    {
                        Name = medicine.Name,
                        Maker = medicine.Maker,
                        Country = medicine.Country,
                        Units = medicine.Units,
                        ShelfLife = medicine.ShelfLife,
                        Description = medicine.Description,
                        Quantity = pharmMedicine.Quantity,
                        Price = pharmMedicine.Price,
                        Pharmacy = pharmacy as Institution
                    };
                }
            }

            // if no content retun 204
            if (result == null)
                return new StatusCodeResult(204);

            // return JSON serialized content
            return new JsonResult(result);
        }
    }
}
