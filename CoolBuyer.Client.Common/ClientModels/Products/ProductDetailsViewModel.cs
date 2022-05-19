using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.ClientModels.Products
{
    public class ProductDetailsViewModel
    {
        public ProductDetailsViewModel()
        {
            this.Product = new ProductViewModel();
            this.Images = new List<ProductImageDetailsViewModel>();
        }

        public ProductViewModel Product { get; set; }
        public List<ProductImageDetailsViewModel> Images { get; set; }
    }

    public class ProductViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime PostedDate { get; set; }
        public int CategoryId { get; set; }
        public int TotalComments { get; set; }
        public int TotalDislikes { get; set; }
        public int TotalLikes { get; set; }
        public int TotalTimesFavorited { get; set; }
        public string CategoryName { get; set; }
        public int SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public bool IsFavorited { get; set; }
        public string PreferredCurrency { get; set; }
        public bool? LikeDislike { get; set; }
    }

    public class ProductImageDetailsViewModel
    {
        public string Uri { get; set; }
        public string Name { get; set; }
    }
}
