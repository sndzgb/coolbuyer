using CoolBuyer.Client.Common.ClientModels.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.ClientModels.Users
{
    public class UserProfileIndexDetailsViewModel
    {
        public UserProfileIndexDetailsViewModel()
        {
            this.UserProfile = new UserProfileDetailsViewModel();
            this.Products = new List<ProductIndexDetailsViewModel>();
        }

        public UserProfileDetailsViewModel UserProfile { get; set; }
        public List<ProductIndexDetailsViewModel> Products { get; set; }
        public PagerViewModel Pagination { get; set; }
    }

    public class UserProfileDetailsViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string ProfileThumbnailImageURI { get; set; }
        public string ProfileThumbnailImageName { get; set; }
        public int TotalActiveProducts { get; set; }
    }

    public class ProductIndexDetailsViewModel
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
