using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.Models
{
    [Table("ProductsLikesDislikes")]
    public class ProductLikeDislikeModel
    {
        public UserModel User { get; set; }
        [Key]
        [Column(Order = 0)]
        public int UserId { get; set; }
        public ProductModel Product { get; set; }
        [Key]
        [Column(Order = 1)]
        public int ProductId { get; set; }
        public bool? LikeDislike { get; set; }
    }
}
