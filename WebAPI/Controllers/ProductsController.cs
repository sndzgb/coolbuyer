using CoolBuyer.Server.WebApi.Controllers.Base;
using CoolBuyer.Server.WebApi.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Text;
using CoolBuyer.Server.WebApi.Results;
using CoolBuyer.Server.BusinessLogic.Services;
using CoolBuyer.Server.BusinessLogic.Common.DTOs.Request.Write;
using CoolBuyer.Server.WebApi.WebApiModels.Products;
using System.IO;
using CoolBuyer.Server.BusinessLogic.Common.DTOs.ResponseModels;
using CoolBuyer.Server.WebApi.Services;
using CoolBuyer.Server.BusinessLogic.Common.DTOs.Request.Read;

namespace CoolBuyer.Server.WebApi.Controllers
{
    [AuthenticationFilter]
    [RoutePrefix("api/products")]
    public class ProductsController : CoolBuyerApiControllerBase
    {
        private readonly IProductService productService;
        private readonly IUserUploadsPathsService uploadsPathsService;
        private readonly IMapper mapper;


        public ProductsController(IProductService productService, 
            IUserUploadsPathsService uploadsPathsService,
            IMapper mapper)
        {
            this.productService = productService;
            this.uploadsPathsService = uploadsPathsService;
            this.mapper = mapper;
        }


        [HttpDelete]
        [Route("{productId}")]
        public IHttpActionResult DeleteProduct([FromUri] int productId)
        {
            productService.Delete(productId);
            return Ok();
        }
        
        [HttpPost]
        public IHttpActionResult PostProduct()
        {
            try
            {
                var request = HttpContext.Current.Request;
                // skip HTML validation
                var form = request.Unvalidated.Form;

                if (!form.AllKeys.Contains("json"))
                {
                    return new MalformedRequestResult(Request, "Request not formed properly.");
                }

                string requestFormJson = form.GetValues("json").First();
                var model = JsonConvert.DeserializeObject<NewProductDTO>(requestFormJson);

                for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                {
                    model.Images.Add(new CreateProductImageDTO()
                    {
                        Name = HttpContext.Current.Request.Files[i].FileName,
                        Stream = HttpContext.Current.Request.Files[i].InputStream
                    });
                }

                // manually validate the incoming model
                ModelState.Clear();
                Validate(model);

                if (!ModelState.IsValid)
                {
                    return new InvalidModelStateResult(ModelState, Request);
                }

                productService.Create(model);
                return Created<object>("", null);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                throw;
            }
        }

        [HttpPut]
        [Route("{productId}")]
        public IHttpActionResult PutProductDetails(int productId, UpdateProductDTO product)
        {
            productService.Update(product);
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{productId}")]
        public IHttpActionResult GetProductDetails(int productId)
        {
            var productDetails = productService.GetSingleProductDetails(productId);

            object[] parameters = new object[] 
            {
                Path.Combine(Request.RequestUri.GetLeftPart(UriPartial.Authority),
                                uploadsPathsService.GetProductImagesStaticPathPart())
            };

            var product = mapper.Map<ProductDetailsApiModel, ProductDetailsDTO>(productDetails, parameters);
            
            return Ok(product);
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetSearchProducts([FromUri] ProductsSearchOptionsDTO model)
        {
            var products = productService.SearchProducts(model);

            object[] parameters = new object[]
            {
                Path.Combine(Request.RequestUri.GetLeftPart(UriPartial.Authority),
                                uploadsPathsService.GetProductImagesStaticPathPart()),
                model
            };

            var mappedProducts = mapper.Map<ProductSearchApiModel, ProductSearchDTO>(products, parameters);

            return Ok(mappedProducts);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("index")]
        public IHttpActionResult GetIndexProducts()
        {
            var products = productService.GetIndexPageProducts();

            object[] parameters = new object[]
            {
                Path.Combine(Request.RequestUri.GetLeftPart(UriPartial.Authority),
                                uploadsPathsService.GetProductImagesStaticPathPart())
            };

            var mappedProducts = mapper.Map<IndexPageProductDetailsApiModel, IndexPageProductDetailsDTO>(products, parameters);
            
            return Ok(mappedProducts);
        }



        #region mapping
        
        /// <summary>
        /// Returns complete Uri for an image represented as a string.
        /// </summary>
        /// <param name="basePath">This param should contain scheme, authority and the port number.</param>
        /// <param name="url">This is the variable portion of the path.</param>
        /// <returns></returns>
        private string MapImageToUri(string basePath, string url)
        {
            var staticPathPart = uploadsPathsService.GetProductImagesStaticPathPart();
            //.GetProductImagesBaseDirectory();
            //var staticPathPart = BusinessLogic.Common.Constants.UploadDirectories.GetProductImagesBaseDirectory();
            var leftPart = new Uri(basePath, UriKind.Absolute);
            var rightPart = new Uri(Path.Combine(staticPathPart, url), UriKind.Relative);
            var uri = new Uri(leftPart, rightPart).ToString();

            return uri;
        }
        
        #endregion
    }
}
