using CoolBuyer.Server.BusinessLogic.Common.DTOs.Request.Write;
using CoolBuyer.Server.BusinessLogic.Services;
using CoolBuyer.Server.WebApi.Controllers.Base;
using CoolBuyer.Server.WebApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoolBuyer.Server.WebApi.Controllers
{
    [RoutePrefix("api/favorites")]
    [AuthenticationFilter]
    public class FavoritesController : CoolBuyerApiControllerBase
    {
        private readonly IFavoritesService favoritesService;


        public FavoritesController(IFavoritesService favoritesService)
        {
            this.favoritesService = favoritesService;
        }

        
        [HttpGet]
        public IHttpActionResult GetFavorites(int take = 5, int page = 1)
        {
            var favorites = favoritesService.Get(take, page);
            return Ok(favorites);
        }
        
        [HttpPost]
        [Route("")]
        public IHttpActionResult PostAddOrRemoveFavorite(AddProductToFavoritesDTO model)
        {
            favoritesService.Add(model);
            return Ok();
        }
    }
}
