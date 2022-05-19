using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolBuyer.Server.WebApi.WebApiModels.Products
{
    public class IndexPageProductDetailsApiModel
    {
        public IndexPageProductDetailsApiModel()
        {
            this.Categories = new List<LatestProductsByCategoryApiModel>();
        }

        public List<LatestProductsByCategoryApiModel> Categories { get; set; }
    }

    public class LatestProductsByCategoryApiModel
    {
        public LatestProductsByCategoryApiModel()
        {
            this.Products = new List<ProductInfoDetailsApiModel>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryThumbnailURI { get; set; }
        public List<ProductInfoDetailsApiModel> Products { get; set; }
    }

    public class ProductInfoDetailsApiModel
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