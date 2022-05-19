using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolBuyer.Server.BusinessLogic.Common.DTOs.Read;
using CoolBuyer.Server.BusinessLogic.Common.DTOs.Write;
using CoolBuyer.Server.BusinessLogic.Database;
using CoolBuyer.Server.BusinessLogic.Common.Exceptions;
using CoolBuyer.Server.BusinessLogic.Common.Models;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.ComponentModel.DataAnnotations;

namespace CoolBuyer.Server.BusinessLogic.Services
{
    public class ProductCommentService : IProductCommentService
    {
        private readonly ICoolBuyerDbContext dbContext;
        private readonly ICurrentUserService currentUserService;

        public ProductCommentService(ICoolBuyerDbContext dbContext, ICurrentUserService currentUserService)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }

            if (currentUserService == null)
            {
                throw new ArgumentNullException("currentUserService");
            }

            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
        }


        public void Create(NewProductCommentDTO newComment, int userId)
        {
            var product = dbContext.Products.Where(x => x.Id == newComment.ProductId).FirstOrDefault();

            if (product == null)
            {
                throw new NotFoundException("Product not found.");
            }

            // check if parent comment exists
            if (newComment.ParentCommentId != null)
            {
                var parentComment = dbContext
                    .Products
                    .Include(q => q.Comments)
                    .SelectMany(
                        x => x.Comments
                        .Where(w => w.ProductId == newComment.ProductId && w.Id == newComment.ParentCommentId
                    )).SingleOrDefault();

                if (parentComment == null)
                {
                    throw new NotFoundException("Parent comment not found.");
                }
            }

            var comment = new ProductCommentModel()
            {
                CommentText = newComment.CommentText,
                ParentCommentId = newComment.ParentCommentId,
                ProductId = newComment.ProductId,
                UserId = userId
            };

            product.Comments.Add(comment);
            dbContext.Commit();
        }

        public void Delete(int commentId, int userId)
        {
            var comment = dbContext
                    .Products
                    .Include(q => q.Comments)
                    .SelectMany(
                        x => x.Comments
                        .Where(w => w.Id == commentId))
                    .SingleOrDefault();

            if (comment.UserId != userId)
            {
                throw new OperationNotAllowedException("Access denied.");
            }

            comment.IsDeleted = true;
            dbContext.Commit();
        }

        public void EditUpdate(UpdateProductCommentDTO updatedComment, int userId)
        {
            var comment = dbContext
                    .Products
                    .Include(q => q.Comments)
                    .SelectMany(
                        x => x.Comments
                        .Where(w => w.Id == updatedComment.CommentId))
                    .SingleOrDefault();

            if (comment == null)
            {
                throw new NotFoundException("Comment not found.");
            }

            if (comment.UserId != userId)
            {
                throw new OperationNotAllowedException("Access denied.");
            }

            comment.CommentText = updatedComment.CommentText;
            comment.LastUpdated = DateTime.UtcNow;

            var validationContext = new ValidationContext(comment, serviceProvider: null, items: null);
            Validator.ValidateObject(comment, validationContext, true);

            dbContext.Commit();
        }
        
        public IEnumerable<ProductCommentDetailsDTO> GetMultiple(FilterProductCommentsDTO model)
        {
            var comments = dbContext.Database.SqlQuery<ProductCommentDetailsDTO>($@"
                EXEC usp_GetProductComments 
                    @ProductId, @CommentId, @Take, @Page",
                    new SqlParameter[] 
                    {
                        new SqlParameter("@ProductId", model.ProductId),
                        new SqlParameter("@CommentId", model.CommentId ?? SqlInt32.Null),
                        new SqlParameter("@Take", model.Take),
                        new SqlParameter("@Page", model.Page)
                    }
            ).ToList();

            return comments;
        }

        public ProductCommentDetailsDTO GetSingle(int productId, int commentId)
        {
            var comment = dbContext
                    .Products
                    .Include(q => q.Comments)
                    .SelectMany(
                        x => x.Comments
                        .Where(w => w.ProductId == productId && w.Id == commentId
                    ))
                    .Select(t => new ProductCommentDetailsDTO
                    {
                        CommentText = t.CommentText,
                        Id = t.Id,
                        LastUpdated = t.LastUpdated,
                        PostedDate = t.PostedDate,
                        TotalDislikes = t.TotalDislikes,
                        TotalLikes = t.TotalLikes,
                        UserId = t.UserId
                    })
                    .SingleOrDefault();
            return comment;
        }

        public void LikeDislikeComment(ProductCommentReactionDTO model, int userId)
        {
            try
            {
                var result = dbContext.Database.ExecuteSqlCommand($@"
                    EXEC usp_LikeDislikeRemoveProductCommentReaction
                        @ProductId, @UserId, @CommentId, @LikeDislike",
                    new SqlParameter[]
                    {
                        new SqlParameter("@ProductId", model.ProductId),
                        new SqlParameter("@UserId", userId),
                        new SqlParameter("@CommentId", model.CommentId),
                        new SqlParameter("@LikeDislike", model.LikeDislike)
                    });

                return;
            }
            catch (SqlException e)
            {
                throw new DbException(e.Message);
            }
        }

        public void FlagAsInappropriate(int commentId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
