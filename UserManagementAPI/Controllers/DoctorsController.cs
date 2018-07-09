using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Doctors")]
    [Authorize]
    public class DoctorsController : Controller
    {
        /// <summary>
        /// Stored procedure executer
        /// </summary>
        private readonly SpExecuter spExecuter;

        /// <summary>
        /// Creates new instance of user controller
        /// </summary>
        public DoctorsController()
        {
            this.spExecuter = new SpExecuter("(local)", "UsersDB", true);
        }


        [HttpGet]
        public IEnumerable<Doctor> Get()
        {
            return this.spExecuter.ExecuteSp<Doctor>("uspGetAllDoctors");
        }

        [HttpGet("{id}", Name = "GetDoctor")]
        public Doctor GetDoctor(int id)
        {
            return this.spExecuter.ExecuteEntitySp<Doctor>(
                "uspGetDoctorById", 
                new[] 
                {
                    new KeyValuePair<string, object>("id", id)
                });
        }

        [HttpPost]
        public void Post([FromBody]Doctor doctor)
        {
            // var userId = int.Parse(User.Claims.Where(claim => claim.Type == "user_id").First().Value);

            this.spExecuter.ExecuteSpNonQuery("uspCreateDoctor",
                new[] 
                {
                    new KeyValuePair<string, object>("userId", 2),
                    new KeyValuePair<string, object>("license", doctor.License),
                    new KeyValuePair<string, object>("hospitalId", doctor.HospitalId)
                });
        }
        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Doctor doctor)
        {
            this.spExecuter.ExecuteSpNonQuery("uspUpdateDoctor",
                new[]
                {
                    new KeyValuePair<string, object>("id", id),
                    new KeyValuePair<string, object>("license", doctor.License),
                    new KeyValuePair<string, object>("hospitalId", doctor.HospitalId)
                });
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.spExecuter.ExecuteSpNonQuery("uspDeleteDoctor",
                new[]
                {
                    new KeyValuePair<string, object>("id", id)
                });
        }
    }
}
