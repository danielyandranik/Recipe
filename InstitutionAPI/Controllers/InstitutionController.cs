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


namespace InstitutionAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Institutions")]
    [Authorize]
    public class InstitutionController : Controller
    {
        /// <summary>
        /// Stored procedure executer
        /// </summary>
        private readonly SpExecuter spExecuter;

        /// <summary>
        /// Creates new instance of institution controller
        /// </summary>
        public InstitutionController()
        {
            this.spExecuter = new SpExecuter("(local)", "InstitutionDB", true);
        }

        [HttpGet]
        public IEnumerable<Institution> Get(string type)
        {
            return this.spExecuter.ExecuteSp<Institution>(
                "GetInstitutions",
                 new[]
                {
                    new KeyValuePair<string, object>("Type", type)
                });
        }

        [HttpGet("{id}")]
        public Institution Get(int id)
        {
            return this.spExecuter.ExecuteEntitySp<Institution>(
                "GetInstitution",
                new[]
                {
                    new KeyValuePair<string, object>("Id", id)
                });
        }

        [HttpPost]
        [Authorize(Policy = "HighLevel")]
        public void Post([FromBody]Institution institution)
        {
            this.spExecuter.ExecuteSpNonQuery(
                                    "AddInstitution",
                                    new[]
                                    {
                                        new KeyValuePair<string, object>("Name", institution.Name),
                                        new KeyValuePair<string, object>("License", institution.License),
                                        new KeyValuePair<string, object>("Owner", institution.Owner),
                                        new KeyValuePair<string, object>("Phone", institution.Phone),
                                        new KeyValuePair<string, object>("Email", institution.Email),
                                        new KeyValuePair<string, object>("Description", institution.Description),
                                        new KeyValuePair<string, object>("OpenTime", institution.OpenTime),
                                        new KeyValuePair<string, object>("CloseTime", institution.CloseTime),
                                        new KeyValuePair<string, object>("Type", institution.Type),
                                        new KeyValuePair<string, object>("Country", institution.Address.Country),
                                        new KeyValuePair<string, object>("State", institution.Address.State),
                                        new KeyValuePair<string, object>("City", institution.Address.City),
                                        new KeyValuePair<string, object>("phone", institution.Address.PostalCode),
                                        new KeyValuePair<string, object>("email", institution.Address.AddressLine)
                                    });
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "InstitutionAdminProfile,HighLevel")]
        public void Put(int id, [FromBody]Institution institution)
        {
            this.spExecuter.ExecuteSpNonQuery("UpdateInstitution",
                                    new[]
                                    {
                                        new KeyValuePair<string, object>("Id", id),
                                        new KeyValuePair<string, object>("Name", institution.Name),
                                        new KeyValuePair<string, object>("License", institution.License),
                                        new KeyValuePair<string, object>("Owner", institution.Owner),
                                        new KeyValuePair<string, object>("Phone", institution.Phone),
                                        new KeyValuePair<string, object>("Email", institution.Email),
                                        new KeyValuePair<string, object>("Description", institution.Description),
                                        new KeyValuePair<string, object>("OpenTime", institution.OpenTime),
                                        new KeyValuePair<string, object>("CloseTime", institution.CloseTime),
                                        new KeyValuePair<string, object>("Country", institution.Address.Country),
                                        new KeyValuePair<string, object>("State", institution.Address.State),
                                        new KeyValuePair<string, object>("City", institution.Address.City),
                                        new KeyValuePair<string, object>("PostalCode", institution.Address.PostalCode),
                                        new KeyValuePair<string, object>("AddressLine", institution.Address.AddressLine)
                                    });
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "HighLevel")]
        public void Delete(int id)
        {
            this.spExecuter.ExecuteSpNonQuery("DeleteInstitution",
                new[]
                {
                    new KeyValuePair<string, object>("Id", id)
                });
        }
    }
}



