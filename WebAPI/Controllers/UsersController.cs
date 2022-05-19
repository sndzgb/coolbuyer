using CoolBuyer.Server.BusinessLogic.Common.DTOs.ResponseModels;
using CoolBuyer.Server.BusinessLogic.Services;
using CoolBuyer.Server.WebApi.Controllers.Base;
using CoolBuyer.Server.WebApi.Services;
using CoolBuyer.Server.WebApi.WebApiModels.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoolBuyer.Server.WebApi.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : CoolBuyerApiControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly IUserUploadsPathsService userUploadsPathsService;


        public UsersController(IUserService userService, IMapper mapper, IUserUploadsPathsService userUploadsPathsService)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.userUploadsPathsService = userUploadsPathsService;
        }

        
        [HttpGet]
        [AllowAnonymous]
        [Route("{id}")]
        public IHttpActionResult GetProfileIndexDetails(int id, int take = 5, int page = 1)
        {
            var profile = userService.GetProfileIndexDetails(id, take, page);

            object[] parameters = new object[]
            {
                Path.Combine(Request.RequestUri.GetLeftPart(UriPartial.Authority),
                                userUploadsPathsService.GetProfileImagesStaticPathPart()),
                Path.Combine(Request.RequestUri.GetLeftPart(UriPartial.Authority),
                                userUploadsPathsService.GetProductImagesStaticPathPart())
            };

            var mappedProfile = mapper.Map<UserProfileIndexDetailsApiModel, UserProfileIndexDetailsDTO>(profile, parameters);

            return Ok(mappedProfile);
        }
    }
}
