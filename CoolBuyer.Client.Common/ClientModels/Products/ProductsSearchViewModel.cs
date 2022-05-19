using CoolBuyer.Client.Common.ClientModels.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.ClientModels.Products
{
    //public class ProductsSearchViewModel
    //{
    //    public ProductsSearchViewModel()
    //    {

    //    }

    //    public string Title { get; set; }
    //    public string Description { get; set; }
    //    public decimal Price { get; set; }
    //    public int CategoryId { get; set; }
    //}


    public class ProductsSearchViewModel
    {
        public ProductsSearchViewModel()
        {
            this.Products = new List<ProductSearchDetailsViewModel>();
            this.Stats = new ProductSearchStatisticsViewModel();
            this.SearchOptions = new ProductsSearchOptionsViewModel();
        }

        public ProductsSearchOptionsViewModel SearchOptions { get; set; }
        public List<ProductSearchDetailsViewModel> Products { get; set; }
        public ProductSearchStatisticsViewModel Stats { get; set; }
        public PagerViewModel Pagination { get; set; }
    }

    public class ProductSearchDetailsViewModel
    {
        public ProductSearchDetailsViewModel()
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

    public class ProductSearchStatisticsViewModel
    {
        public ProductSearchStatisticsViewModel()
        {
            this.CategoryTotals = new List<CategoryTotalsViewModel>();
            this.SubcategoryTotals = new List<SubcategoryTotalsViewModel>();
            this.SectionTotals = new List<SectionTotalsViewModel>();
        }

        public int TotalItemsFound { get; set; }
        public List<CategoryTotalsViewModel> CategoryTotals { get; set; }
        public List<SubcategoryTotalsViewModel> SubcategoryTotals { get; set; }
        public List<SectionTotalsViewModel> SectionTotals { get; set; }
    }

    public class SectionTotalsViewModel
    {
        public int? SectionId { get; set; }
        public string Name { get; set; }
        public int? TotalFound { get; set; }
        public int? SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public string CategoryName { get; set; }
        public int? CategoryId { get; set; }
    }

    public class SubcategoryTotalsViewModel
    {
        public int? TotalFound { get; set; }
        public int? SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public string CategoryName { get; set; }
        public int? CategoryId { get; set; }
    }

    public class CategoryTotalsViewModel
    {
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? TotalFound { get; set; }
    }
}
