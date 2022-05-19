using CoolBuyer.Client.Common.ApiEndpoints.EndpointsAbstractions;
using CoolBuyer.Client.Common.ClientModels.Products;
using CoolBuyer.Client.Web.MVC.Controllers.BaseController;
using CoolBuyer.Client.Web.MVC.ObjectModelBinders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CoolBuyer.Client.Web.MVC.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : BaseCoolBuyerController
    {
        private readonly IProductsEndpoint productsEndpoint;


        public ProductsController(IProductsEndpoint productsEndpoint)
        {
            this.productsEndpoint = productsEndpoint;
        }

        
        [HttpGet]
        [AllowAnonymous]
        [ActionName("index")]
        public async Task<ActionResult> Index()
        {
            var products = await productsEndpoint.GetIndexPageProductsAsync();
            return View(products);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("details/{id}")]
        public async Task<ActionResult> Details(int id)
        {
            var product = await productsEndpoint.GetSingleAsync(id);

            return View(product);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("search")]
        public async Task<ActionResult> Search(ProductsSearchOptionsViewModel model)
        {
            var products = await productsEndpoint.SearchAsync(model);

            return View(products);
        }

        [HttpGet]
        [Route("update/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            var product = await productsEndpoint.GetSingleAsync(id);

            return View(product);
        }

        [HttpPost]
        [Route("update/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UpdateProductDetailsViewModel model)
        {
            await productsEndpoint.UpdateAsync(id, model);

            return View();
        }

        [HttpPost]
        [Route("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            await productsEndpoint.DeleteAsync(id);

            return View();
        }

        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {
            //Dictionary<string, string> categories = new Dictionary<string, string>()
            //{
            //    { "Category 1", "1" },
            //    { "Category 2", "2" },
            //    { "Category 3", "3" }
            //};
            //IEnumerable<SelectListItem> categories1 = categories.Select(x => new SelectListItem()
            //{
            //    Value = x.Value,
            //    Text = x.Key
            //}).ToList();

            ////categories.Insert(0, new SelectListItem() { Value = "0", Text = "All" });
            //ViewBag.Categories = categories1;
            return View();
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create([ModelBinder(typeof(CreateProductModelBinder))]NewProductViewModel model)
        {
            ModelState.Clear();
            
            if (!TryValidateModel(model))
            {
                return View(model);
            }
            
            await productsEndpoint.CreateAsync(model);

            return RedirectToAction("index", "home");
        }
    }
}
