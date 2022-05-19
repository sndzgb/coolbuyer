using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.DTOs.ResponseModels
{
    public class ProductSearchDTO
    {
        public ProductSearchDTO()
        {
            this.Products = new List<ProductSearchDetailsDTO>();
            this.Stats = new ProductSearchStatisticsDTO();
        }

        public List<ProductSearchDetailsDTO> Products { get; set; }
        public ProductSearchStatisticsDTO Stats { get; set; }
    }

    public class ProductSearchDetailsDTO
    {
        public ProductSearchDetailsDTO()
        {
            //this.User = new UserDetailsDTO();
        }

        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }        
        public int SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public int? SectionId { get; set; }
        public string SectionName { get; set; }
        public int TotalComments { get; set; }
        public int TotalLikes { get; set; }
        public int TotalDislikes { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Price { get; set; }
        public string ProductThumbnailImage { get; set; }

        // user
        public int UserId { get; set; }
        public string Username { get; set; }
        public bool IsAccountConfirmed { get; set; }
        //public string UserProfileThumbnailImageName { get; set; }
        //
        //public string ThumbnailUrl { get; set; }

        //public UserDetailsDTO User { get; set; }
    }

    //public class UserDetailsDTO
    //{
    //    public int UserId { get; set; }
    //    public string Username { get; set; }
    //    public bool IsAccountConfirmed { get; set; }
    //    public string ThumbnailImageName { get; set; }
    //}

    public class ProductSearchStatisticsDTO
    {
        public ProductSearchStatisticsDTO()
        {
            this.CategoryTotals = new List<CategoryTotalsDTO>();
            this.SubcategoryTotals = new List<SubcategoryTotalsDTO>();
            this.SectionTotals = new List<SectionTotalsDTO>();
        }

        public int TotalItemsFound { get; set; }
        public List<CategoryTotalsDTO> CategoryTotals { get; set; }
        public List<SubcategoryTotalsDTO> SubcategoryTotals { get; set; }
        public List<SectionTotalsDTO> SectionTotals { get; set; }
    }

    public class SectionTotalsDTO
    {
        public int? SectionId { get; set; }
        public string Name { get; set; }
        public int? TotalFound { get; set; }
        public int? SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public string CategoryName { get; set; }
        public int? CategoryId { get; set; }
    }

    public class SubcategoryTotalsDTO
    {
        public int? TotalFound { get; set; }
        public int? SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public string CategoryName { get; set; }
        public int? CategoryId { get; set; }
    }

    public class CategoryTotalsDTO
    {
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? TotalFound { get; set; }
    }
}
