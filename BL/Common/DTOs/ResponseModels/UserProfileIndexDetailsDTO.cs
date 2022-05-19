using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.DTOs.ResponseModels
{
    public class UserProfileIndexDetailsDTO
    {
        public UserProfileIndexDetailsDTO()
        {
            this.UserProfile = new UserProfileDetailsDTO();
            this.Products = new List<ProductIndexDetailsDTO>();
        }

        public UserProfileDetailsDTO UserProfile { get; set; }
        public List<ProductIndexDetailsDTO> Products { get; set; }
    }

    public class UserProfileDetailsDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string ProfileThumbnailImageName { get; set; } //ProfileThumbnailUrl
        public string ProfileThumbnailImageUrl { get; set; }
        public int TotalActiveProducts { get; set; }
    }

    public class ProductIndexDetailsDTO
    {
        public int ProductId { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ProductThumbnailImageName { get; set; }
        public string ProductThumbnailImageUrl { get; set; }
        //public string ProductThumbnailUrl { get; set; }
        public int TotalComments { get; set; }
        public int TotalLikes { get; set; }
        public int TotalDislikes { get; set; }
        public int TotalTimesFavorited { get; set; }
    }
}
