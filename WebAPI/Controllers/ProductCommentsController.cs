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
    [AuthenticationFilter]
    [RoutePrefix("api/products/{productId}/comments")]
    public class ProductCommentsController : ApiController
    {
        private readonly IProductCommentService productCommentService;
        private readonly ICurrentUserService currentUserService;


        public ProductCommentsController(IProductCommentService productCommentService, ICurrentUserService currentUserService)
        {
            this.productCommentService = productCommentService;
            this.currentUserService = currentUserService;
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("{commentId}")]
        public IHttpActionResult GetProductCommentById(int productId, int commentId)
        {
            var comment = productCommentService.GetSingle(productId, commentId);
            return Ok(comment);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("")]
        public IHttpActionResult GetProductComments([FromUri] BusinessLogic.Common.DTOs.Read.FilterProductCommentsDTO model)
        {
            var comments = productCommentService.GetMultiple(model);
            return Ok(comments);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult PostProductComment(BusinessLogic.Common.DTOs.Write.NewProductCommentDTO comment)
        {
            productCommentService.Create(comment, currentUserService.Id);
            return Created<object>("", null);
        }
        
        [HttpPost]
        [Route("{commentId}/react")]
        public IHttpActionResult PostCommentLikeDislike(BusinessLogic.Common.DTOs.Write.ProductCommentReactionDTO model)
        {
            productCommentService.LikeDislikeComment(new BusinessLogic.Common.DTOs.Write.ProductCommentReactionDTO()
            {
                CommentId = model.CommentId,
                LikeDislike = model.LikeDislike,
                ProductId = model.ProductId
            },
            currentUserService.Id);
            return Ok();
        }
        
        [HttpPut]
        [Route("{commentId}")]
        public IHttpActionResult PutProductComment(BusinessLogic.Common.DTOs.Write.UpdateProductCommentDTO comment, int commentId)
        {
            comment.CommentId = commentId;
            productCommentService.EditUpdate(comment, currentUserService.Id);
            return Ok();
        }

        [HttpPost]
        [Route("{commentId}")]
        public IHttpActionResult DeleteProductComment(int commentId)
        {
            productCommentService.Delete(commentId, currentUserService.Id);
            return Ok();
        }        
    }
}
