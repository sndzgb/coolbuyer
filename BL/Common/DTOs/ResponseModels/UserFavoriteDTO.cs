using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.DTOs.ResponseModels
{
    public class UserFavoriteDTO
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string ProductThumbnailUrl { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
