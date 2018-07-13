using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAccess;
using DatabaseAccess.SpExecuters;
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
        
    }
}
