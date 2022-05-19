using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.ClientModels.Products
{
    public class ProductsSearchOptionsViewModel
    {
        public int Take { get; set; } = 10;
        public int Page { get; set; } = 1;
        public string SortBy { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string SearchString { get; set; }
        public int? CategoryId { get; set; }
        public int? SubcategoryId { get; set; }
        public int? SectionId { get; set; }
    }
}
