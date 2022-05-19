using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.Models
{
    [Table("Users")]
    public class UserModel
    {

        public UserModel()
        {
            this.Favorites = new HashSet<FavoriteProductModel>();
            this.Products = new HashSet<ProductModel>();

            this.CommentsReactions = new HashSet<CommentLikeDislikeModel>();
            this.ProductsReactions = new HashSet<ProductLikeDislikeModel>();
        }
        

        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string ThumbnailUrl { get; set; }
        public bool IsAccountConfirmed { get; set; }
        public bool IsLockedOut { get; set; }

        public AccountType AccountType { get; set; }
        public int AccountTypeId { get; set; }
        public ICollection<ProductModel> Products { get; set; }
        public ICollection<FavoriteProductModel> Favorites { get; set; }
        public ICollection<CommentLikeDislikeModel> CommentsReactions { get; set; }
        public ICollection<ProductLikeDislikeModel> ProductsReactions { get; set; }
        public ICollection<ProductCommentModel> Comments { get; set; }
    }
}
