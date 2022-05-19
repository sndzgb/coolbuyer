using CoolBuyer.Client.Common.ApiEndpoints.EndpointsAbstractions;
using CoolBuyer.Client.Common.ClientModels.Comments;
using CoolBuyer.Client.Web.MVC.Controllers.BaseController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CoolBuyer.Client.Web.MVC.Controllers
{
    //[Route("products/{productId:int}/comments")]
    [RoutePrefix("products/{productId}/comments")]
    public class ProductCommentsController : BaseCoolBuyerController
    {
        [HttpGet]
        [AllowAnonymous]
        //[ActionName("{productId}/comments/{commentId}")]
        //[Route("{productId}/comments")]
        //[Route("{productId}/asd")]
        //[Route("{productId}/comments")]
        public async Task<ActionResult> GetComments(int productId, int? parentCommentId, int take = 5, int page = 1)
        {
            var productComments = await productCommentsEndpoint.GetMultipleAsync(take, page, productId, parentCommentId);
            //return View(productComments);
            return Json(new { statusCode = 200, comments = productComments }, "application/json", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        //[ActionName("{productId}/comments/{commentId}")]
        [Route("{productId}/comments/create")]
        //[Route("{productId}/asd")]
        //[Route("{productId}/comments")]
        public ActionResult Create(int productId)
        {
            return View("~/Views/ProductComments/ProductComments.cshtml");
        }


        private readonly IProductCommentsEndpoint productCommentsEndpoint;


        public ProductCommentsController(IProductCommentsEndpoint productCommentsEndpoint)
        {
            this.productCommentsEndpoint = productCommentsEndpoint;
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Route("update/{commentId}")]
        //public async Task<ActionResult> Update(UpdateProductCommentViewModel model)
        //{
        //    await productCommentsEndpoint.UpdateAsync(model);

        //    if (Request.IsAjaxRequest())
        //    {
        //        return Json(new { statusCode = 200 }, "application/json", JsonRequestBehavior.DenyGet);
        //    }

        //    return RedirectToAction("details", "products", new { id = model.ProductId });
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Route("delete/{commentId}")]
        //public async Task<ActionResult> Delete(int productId, int commentId)
        //{
        //    await productCommentsEndpoint.DeleteAsync(productId, commentId);
        //    return Json(new { statusCode = 200 }, "application/json", JsonRequestBehavior.DenyGet);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Route("create")]
        //public async Task<ActionResult> AddNew(CreateProductCommentViewModel model)
        //{
        //    await productCommentsEndpoint.CreateAsync(model);
        //    return Json(new { statusCode = 201 }, "application/json", JsonRequestBehavior.DenyGet);
        //}

        //[HttpGet]
        //[AllowAnonymous]
        ////[ActionName("")]
        ////[Route("")]
        //public async Task<ActionResult> GetMultiple(int productId, int? parentCommentId, int take = 5, int page = 1)
        //{
        //    var productComments = await productCommentsEndpoint.GetMultipleAsync(take, page, productId, parentCommentId);
        //    return View(productComments);
        //    return Json(new { statusCode = 200, comments = productComments }, "application/json", JsonRequestBehavior.AllowGet);
        //}

    }
}