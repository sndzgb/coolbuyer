using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.Models
{
    [Table("Products")]
    public class ProductModel
    {

        public ProductModel()
        {
            this.Images = new HashSet<ProductImage>();
            this.Comments = new HashSet<ProductCommentModel>();
            this.ProductLikesDislikes = new HashSet<ProductLikeDislikeModel>();
        }

        public ProductModel(int id)
        {
            this.Images = new HashSet<ProductImage>();
            this.Comments = new HashSet<ProductCommentModel>();
            this.ProductLikesDislikes = new HashSet<ProductLikeDislikeModel>();

            this.Id = id;
        }


        [Key]
        public int Id { get; private set; }

        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "Invalid title length.", MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "Invalid title length.", MinimumLength = 2)]
        public string Description { get; set; }

        public decimal Price { get; set; }
        public int TotalLikes { get; set; }
        public int TotalDislikes { get; set; }
        public int TotalComments { get; set; }
        public int TotalTimesFavorited { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime? LastUpdated { get; set; }

        public UserModel User { get; set; }
        public int UserId { get; set; }
        public CategoryModel Category { get; set; }
        public int? CategoryId { get; set; }
        public SubcategoryModel Subcategory { get; set; }
        public int? SubcategoryId { get; set; }
        public SectionModel Section { get; set; }
        public int? SectionId { get; set; }
        public ICollection<ProductCommentModel> Comments { get; set; }
        public ICollection<ProductLikeDislikeModel> ProductLikesDislikes { get; set; }
        public ICollection<ProductImage> Images { get; set; }
    }
}
