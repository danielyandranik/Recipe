using System.Threading.Tasks;
using MedicineAPI.Models;
using MedicineAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace MedicineAPI.Controllers
{
	[Produces("application/json")]
    [Route("api/medicines")]
    public class MedicineController : Controller
    {
        private readonly IMedicineRepository _medicineRepository;

        public MedicineController(IMedicineRepository medicineRepository)
        {
            this._medicineRepository = medicineRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = this.Request.Query;
            if (query.Count > 0)
            {
                StringValues queryString;

                if (query.TryGetValue("country", out queryString))
                {
                    var medicines = await this._medicineRepository.GetByCountry(queryString[0]);

                    if (medicines == null)
                    {
                        return new NotFoundResult();
                    }

                    return new ObjectResult(medicines);
                }

                if (query.TryGetValue("medicineName", out queryString))
                {
                    var medicine = await this._medicineRepository.GetMedicineByName(queryString[0]);

                    if (medicine == null)
                    {
                        return new NotFoundResult();
                    }

                    return new ObjectResult(medicine);
                }

                return new NotFoundResult();
            }

            return new ObjectResult(await this._medicineRepository.GetAllMedicines());
        }


        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(string id)
        {
            var medicine = await this._medicineRepository.GetMedicineByID(id);

            if (medicine == null)
            {
                return new NotFoundResult();
            }
            return new ObjectResult(medicine);
        }

        [Authorize(Policy = "MinistryWorkerProfile")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Medicine medicine)
        {
            await this._medicineRepository.Create(medicine);
            return new OkObjectResult(medicine);
        }

        [Authorize(Policy = "MinistryWorkerProfile")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]Medicine medicine)
        {
            var dbRecipe = await this._medicineRepository.GetMedicineByID(id);

            if (dbRecipe == null)
            {
                return new NotFoundResult();
            }

            medicine.Id = dbRecipe.Id;

            await this._medicineRepository.Update(medicine);
            return new OkObjectResult(medicine);
        }

        [Authorize(Policy = "MinistryWorkerProfile")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var dbMedicine = await this._medicineRepository.GetMedicineByID(id);

            if (dbMedicine == null)
            {
                return new NotFoundResult();
            }

            await this._medicineRepository.Delete(id);
            return new OkResult();
        }
    }
}