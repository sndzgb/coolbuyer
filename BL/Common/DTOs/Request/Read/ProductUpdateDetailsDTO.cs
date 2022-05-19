using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.DTOs.Request.Read
{
    public class ProductUpdateDetailsDTO
    {
        public ProductUpdateDetailsDTO()
        {
            this.Product = new ExistingProductDetailsDTO();
            this.Images = new List<ProductImageDetailsDTO>();
        }

        public ExistingProductDetailsDTO Product { get; set; }
        public List<ProductImageDetailsDTO> Images { get; set; }

        //public ProductUpdateDetailsDTO()
        //{
        //    this.Images = new List<ProductImageDetailsDTO>();
        //}

        //public string Title { get; set; }
        //public string Description { get; set; }
        //public int Price { get; set; }
        //public int CategoryId { get; set; }
        //public string CategoryName { get; set; }
        //public int SubcategoryId { get; set; }
        //public string SubcategoryName { get; set; }

        //public List<ProductImageDetailsDTO> Images { get; set; }
    }

    public class ExistingProductDetailsDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }
    }
}
