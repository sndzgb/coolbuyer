using CoolBuyer.Server.BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace CoolBuyer.Server.WebApi.Controllers
{
    [RoutePrefix("uploads")]
    public class UploadsController : ApiController
    {
        private readonly IUserUploadsPathsService uploadsPathsService;


        public UploadsController(IUserUploadsPathsService uploadsPathsService)
        {
            this.uploadsPathsService = uploadsPathsService;
        }

        
        [HttpGet]
        [AllowAnonymous]
        [Route("images/products/{year}/{month}/{day}/{productId}/{imageName}")]
        public HttpResponseMessage GetProductImage(int year, int month, int day, int productId, string imageName)
        {
            FileStream fs = new FileStream(
                    Path.Combine(
                            uploadsPathsService.GetProductImagesStaticPathPart(true),
                            $@"{year}\{month}\{day}\{productId}\{imageName}"
                        ),
                    FileMode.Open
                );
            
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(fs);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            return response;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("images/profiles/{userId}/{imageName}")]
        public HttpResponseMessage GetProfileImage(int userId, string imageName)
        {
            FileStream fs = new FileStream(
                    Path.Combine(
                            uploadsPathsService.GetProfileImagesStaticPathPart(true),
                            userId.ToString(), 
                            imageName
                        ),
                    FileMode.Open
                );

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(fs);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            return response;
        }
    }
}
