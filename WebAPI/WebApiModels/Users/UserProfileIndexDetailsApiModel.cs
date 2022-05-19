using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolBuyer.Server.WebApi.WebApiModels.Users
{
    public class UserProfileIndexDetailsApiModel
    {
        public UserProfileIndexDetailsApiModel()
        {
            this.UserProfile = new UserProfileDetailsApiModel();
            this.Products = new List<ProductIndexDetailsApiModel>();
        }

        public UserProfileDetailsApiModel UserProfile { get; set; }
        public List<ProductIndexDetailsApiModel> Products { get; set; }
    }

    public class UserProfileDetailsApiModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string ProfileThumbnailImageName { get; set; }
        public string ProfileThumbnailImageURI { get; set; }
        public int TotalActiveProducts { get; set; }
    }

    public class ProductIndexDetailsApiModel
    {
        public int ProductId { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ProductThumbnailImageURI { get; set; }
        public string ProductThumbnailImageName { get; set; }
        public int TotalComments { get; set; }
        public int TotalLikes { get; set; }
        public int TotalDislikes { get; set; }
        public int TotalTimesFavorited { get; set; }
    }
}