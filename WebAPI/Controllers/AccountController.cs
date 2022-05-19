using CoolBuyer.Server.Authentication.Managers;
using CoolBuyer.Server.BusinessLogic.Common.DTOs;
using CoolBuyer.Server.BusinessLogic.Services;
using CoolBuyer.Server.WebApi.Security;
using CoolBuyer.Server.WebApi.WebApiModels.Authentication;
using CoolBuyer.Server.WebApi.WebApiModels.Create;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CoolBuyer.Server.WebApi.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private readonly IAccountService accountService;


        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public IHttpActionResult Register([FromBody] NewApplicationUserDTO model)
        {
            accountService.Register(model);
            
            return Created<object>("", null);
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public IHttpActionResult Login(LoginDetailsWebApiModel login)
        {
            var token = accountService.Login(login.Username, login.Password);

            if (token == string.Empty)
            {
                return BadRequest("Login failed.");
            }
            
            return Ok(token);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("confirm-account")]
        public IHttpActionResult ConfirmAccount([FromUri] string token, [FromUri] int userId)
        {
            accountService.ConfirmAccountByEmail(token, userId);
            
            return Ok();
        }       
    }
}
