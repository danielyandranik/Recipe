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
                "uspGetInstitutionsByType",
                 new[]
                {
                    new KeyValuePair<string, object>("type", type)
                });
        }

        [HttpGet("{id}")]
        public Institution Get(int id)
        {
            return this.spExecuter.ExecuteEntitySp<Institution>(
                "uspGetInstitutionById",
                new[]
                {
                    new KeyValuePair<string, object>("id", id)
                });
        }

        [HttpPost]
        [Authorize(Policy = "HighLevel")]
        public void Post([FromBody]Institution institution)
        {
            this.spExecuter.ExecuteSpNonQuery(
                                    "uspCreateInstitution",
                                    new[]
                                    {
                                        new KeyValuePair<string, object>("name", institution.Name),
                                        new KeyValuePair<string, object>("license", institution.License),
                                        new KeyValuePair<string, object>("owner", institution.Owner),
                                        new KeyValuePair<string, object>("phone", institution.Phone),
                                        new KeyValuePair<string, object>("email", institution.Email),
                                        new KeyValuePair<string, object>("description", institution.Description),
                                        new KeyValuePair<string, object>("openTime", institution.OpenTime),
                                        new KeyValuePair<string, object>("closeTime", institution.CloseTime),
                                        new KeyValuePair<string, object>("type", institution.Type),
                                        new KeyValuePair<string, object>("name", institution.Address.Country),
                                        new KeyValuePair<string, object>("license", institution.Address.State),
                                        new KeyValuePair<string, object>("addressId", institution.Address.City),
                                        new KeyValuePair<string, object>("phone", institution.Address.PostalCode),
                                        new KeyValuePair<string, object>("email", institution.Address.AddressLine)
                                    });
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "InstitutionAdminProfile,HighLevel")]
        public void Put(int id, [FromBody]Institution institution)
        {
            this.spExecuter.ExecuteSpNonQuery("uspUpdateInstitution",
                                    new[]
                                    {
                                        new KeyValuePair<string, object>("name", institution.Name),
                                        new KeyValuePair<string, object>("license", institution.License),
                                        new KeyValuePair<string, object>("owner", institution.Owner),
                                        new KeyValuePair<string, object>("phone", institution.Phone),
                                        new KeyValuePair<string, object>("email", institution.Email),
                                        new KeyValuePair<string, object>("description", institution.Description),
                                        new KeyValuePair<string, object>("openTime", institution.OpenTime),
                                        new KeyValuePair<string, object>("closeTime", institution.CloseTime),
                                        new KeyValuePair<string, object>("name", institution.Address.Country),
                                        new KeyValuePair<string, object>("license", institution.Address.State),
                                        new KeyValuePair<string, object>("addressId", institution.Address.City),
                                        new KeyValuePair<string, object>("phone", institution.Address.PostalCode),
                                        new KeyValuePair<string, object>("email", institution.Address.AddressLine)
                                    });
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "HighLevel")]
        public void Delete(int id)
        {
            this.spExecuter.ExecuteSpNonQuery("uspDeleteInstitution",
                new[]
                {
                    new KeyValuePair<string, object>("id", id)
                });
        }
    }
}



