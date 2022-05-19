using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.DTOs.Request.Read
{
    public class ProductsSearchOptionsDTO //ProductsSearchDTO
    {
        public int Take { get; set; } = 10;
        public int Page { get; set; } = 1;
        public string SortBy { get; set; }
        public string SearchString { get; set; }
        public int? CategoryId { get; set; }
        public int? SubcategoryId { get; set; }
        public int? SectionId { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }
}
