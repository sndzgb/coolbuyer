using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.Models
{
    [Table("ProductsComments")]
    public class ProductCommentModel
    {
        // added
        public ProductCommentModel()
        {
            this.CommentsLikesDislikes = new HashSet<CommentLikeDislikeModel>();

            this.PostedDate = DateTime.UtcNow;
        }
        public ProductCommentModel(int productId)
        {
            this.Id = productId;
            this.CommentsLikesDislikes = new HashSet<CommentLikeDislikeModel>();

            this.PostedDate = DateTime.UtcNow;
        }
        public ICollection<CommentLikeDislikeModel> CommentsLikesDislikes { get; set; }
        //

        [Key]
        public int Id { get; private set; }
        public string CommentText { get; set; }
        public int TotalLikes { get; set; }
        public int TotalDislikes { get; set; }
        public bool IsDeleted { get; set; }
        public int? ParentCommentId { get; set; } // ORIGINAL: not null
        public ProductModel Product { get; set; }
        public int ProductId { get; set; }
        public UserModel User { get; set; }
        public int UserId { get; set; }
        public DateTime PostedDate { get; private set; }
        public DateTime? LastUpdated { get; set; }
    }
}
