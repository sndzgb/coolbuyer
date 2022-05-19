using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.Models
{
    [Table("ProductsImages")]
    public class ProductImage
    {
        public ProductModel Product { get; set; }
        [Key]
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }

        [StringLength(maximumLength: 50, ErrorMessage = "Invalid image length.", MinimumLength = 2)]
        public string ImageName { get; set; }
    }
}
