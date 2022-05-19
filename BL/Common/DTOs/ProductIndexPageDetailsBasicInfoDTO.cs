using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.DTOs
{
    public class ProductIndexPageDetailsBasicInfoDTO
    {
        public int? ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public DateTime? PostedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryThumbnailUrl { get; set; }
        public string ProductThumbnailUrl { get; set; }
        public string ProductThumbnailName { get; set; }
    }
}
