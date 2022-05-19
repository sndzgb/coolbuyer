using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.DTOs
{
    public class ProductIndexDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
