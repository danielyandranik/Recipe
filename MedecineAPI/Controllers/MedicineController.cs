using System.Threading.Tasks;
using MedicineAPI.Models;
using MedicineAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

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
            return new ObjectResult(await this._medicineRepository.GetAllMedicines());
        }

        [HttpGet]
        public async Task<IActionResult> GetMedicineByName(string name)
        {
            var medicine = await this._medicineRepository.GetMedicineByName(name);

            if(medicine == null)
            {
                return new NotFoundResult();
            }

            return new ObjectResult(medicine);
        }

        [HttpGet]
        public async Task<IActionResult> GetMedicineByCountry(string name)
        {
            var medicines = await this._medicineRepository.GetByCountry(name);

            if (medicines == null)
            {
                return new NotFoundResult();
            }

            return new ObjectResult(medicines);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var medicine = await this._medicineRepository.GetMedicineByID(id);

            if (medicine == null)
            {
                return new NotFoundResult();
            }
            return new ObjectResult(medicine);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Medicine medicine)
        {
            await this._medicineRepository.Create(medicine);
            return new OkObjectResult(medicine);
        }

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