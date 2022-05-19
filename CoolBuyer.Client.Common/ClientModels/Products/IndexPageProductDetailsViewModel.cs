using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoolBuyer.Client.Common.ClientModels.Products
{
    public class IndexPageProductDetailsViewModel
    {
        public IndexPageProductDetailsViewModel()
        {
            this.Categories = new List<LatestProductsByCategoryViewModel>();
        }

        public List<LatestProductsByCategoryViewModel> Categories { get; set; }
    }

    public class LatestProductsByCategoryViewModel
    {
        public LatestProductsByCategoryViewModel()
        {
            this.Products = new List<ProductInfoViewModel>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryThumbnailURI { get; set; }
        public List<ProductInfoViewModel> Products { get; set; }
    }

    public class ProductInfoViewModel
    {
        public int? ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public DateTime? PostedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string ProductThumbnailURI { get; set; }
        public string ProductThumbnailName { get; set; }
    }
}
