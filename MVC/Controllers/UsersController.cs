using CoolBuyer.Client.Common.ApiEndpoints.EndpointsAbstractions;
using CoolBuyer.Client.Web.MVC.Controllers.BaseController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CoolBuyer.Client.Web.MVC.Controllers
{
    [RoutePrefix("users")]
    public class UsersController : BaseCoolBuyerController
    {
        private IUsersEndpoint usersEndpoint;


        public UsersController(IUsersEndpoint usersEndpoint)
        {
            this.usersEndpoint = usersEndpoint;
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("details/{id}")]
        public async Task<ActionResult> Details(int id, int take = 5, int page = 1)
        {
            var userDetails = await usersEndpoint.GetSingleAsync(id, take, page);

            if (userDetails == null)
            {
                return View("~/Views/Error/NotFound.cshtml");
            }

            userDetails.Pagination = new Common.ClientModels.Pagination.PagerViewModel(userDetails.UserProfile.TotalActiveProducts, page, take);

            return View(userDetails);
        }
        
    }
}