using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.DTOs.ResponseModels
{
    public class IndexPageProductDetailsDTO
    {
        public IndexPageProductDetailsDTO()
        {
            this.Categories = new List<LatestProductsByCategoryDTO>();
        }

        public List<LatestProductsByCategoryDTO> Categories { get; set; }
    }

    public class LatestProductsByCategoryDTO
    {
        public LatestProductsByCategoryDTO()
        {
            this.Products = new List<ProductInfoDetailsDTO>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryThumbnailUrl { get; set; }
        public List<ProductInfoDetailsDTO> Products { get; set; }
    }

    public class ProductInfoDetailsDTO
    {
        public int? ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public DateTime? PostedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string ProductThumbnailUrl { get; set; }
        public string ProductThumbnailName { get; set; }
    }
}
