using CoolBuyer.Server.BusinessLogic.Common.DTOs.Request.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.DTOs.ResponseModels
{
    public class ProductDetailsDTO
    {
        public ProductDetailsDTO()
        {
            this.Product = new ProductInfoDTO();
            //this.User = new UserDetailsDTO();
            //this.Images = new List<ProductImageDetailsDTO>();
            this.Images = new List<ProductImageDTO>();
        }

        //public int ProductId { get; set; }
        //public string Title { get; set; }
        //public int Price { get; set; }
        //public int TotalLikes { get; set; }
        //public int TotalDislikes { get; set; }
        //public DateTime PostedDate { get; set; }

        //public int UserId { get; set; }
        //public string Username { get; set; }

        public ProductInfoDTO Product { get; set; }
        //public UserDetailsDTO User { get; set; }
        public List<ProductImageDTO> Images { get; set; }
        //public List<ProductImageDetailsDTO> Images { get; set; }
    }

    public class ProductImageDTO
    {
        public int ProductId { get; set; }
        public string ImageName { get; set; }
        public string Url { get; set; }
    }

    public class ProductInfoDTO
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

    //public class UserDetailsDTO
    //{
    //    public int UserId { get; set; }
    //    public string Username { get; set; }
    //    public string ThumbnailUrl { get; set; }
    //}

    //public class UserIndexDetailsDTO
    //{
    //    public int UserId { get; set; }
    //    public string Username { get; set; }
    //    public DateTime RegistrationDate { get; set; }
    //    public string Email { get; set; }
    //    public string ThumbnailUrl { get; set; }
    //}
}
