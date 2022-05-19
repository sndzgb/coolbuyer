using CoolBuyer.Client.Common.ApiEndpoints.EndpointsAbstractions;
using CoolBuyer.Client.Common.ClientModels.Favorites;
using CoolBuyer.Client.Web.MVC.Controllers.BaseController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CoolBuyer.Client.Web.MVC.Controllers
{
    [RoutePrefix("favorites")]
    public class FavoritesController : BaseCoolBuyerController
    {
        private readonly IFavoritesEndpoint favoritesEndpoint;


        public FavoritesController(IFavoritesEndpoint favoritesEndpoint)
        {
            this.favoritesEndpoint = favoritesEndpoint;
        }


        [HttpGet]
        [Route("my-favorites")]
        public async Task<ActionResult> ViewFavorites(int take, int page)
        {
            var favorites = await favoritesEndpoint.GetAsync(take, page);

            return View(favorites);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("add-remove")]
        public async Task<ActionResult> AddOrRemoveFavorite(AddOrRemoveFavoriteViewModel model)
        {
            await favoritesEndpoint.AddOrRemoveAsync(model);

            if (AjaxRequestExtensions.IsAjaxRequest(Request))
            {
                return Json(new { statusCode = 200, message = "Done." }, "application/json", JsonRequestBehavior.DenyGet);
            }

            // return original page
            return View($"~/products/details/{model.ProductId}");
        }
    }
}
