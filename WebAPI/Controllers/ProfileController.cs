using CoolBuyer.Server.BusinessLogic.Services;
using CoolBuyer.Server.WebApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoolBuyer.Server.WebApi.Controllers
{
    public class ProfileController : ApiController
    {
        private readonly IProfileService profileService;


        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }


        [HttpGet]
        [AuthenticationFilter]
        public IHttpActionResult GetProfileDetails(int userId)
        {
            return Ok();
        }
    }
}
