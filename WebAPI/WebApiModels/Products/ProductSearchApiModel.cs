using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolBuyer.Server.WebApi.WebApiModels.Products
{
    public class ProductSearchApiModel
    {
        public ProductSearchApiModel()
        {
            this.Products = new List<ProductSearchDetailsApiModel>();
            this.Stats = new ProductSearchStatisticsApiModel();
            this.SearchParameters = new ProductsSearchOptionsApiModel();
        }

        public List<ProductSearchDetailsApiModel> Products { get; set; }
        public ProductSearchStatisticsApiModel Stats { get; set; }
        public ProductsSearchOptionsApiModel SearchParameters { get; set; }
    }

    public class ProductsSearchOptionsApiModel
    {
        public int Take { get; set; }
        public int Page { get; set; }
        public string SortBy { get; set; }
        public string SearchString { get; set; }
        public int? CategoryId { get; set; }
        public int? SubcategoryId { get; set; }
        public int? SectionId { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }

    public class ProductSearchDetailsApiModel
    {
        public ProductSearchDetailsApiModel()
        {
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
        public string ProductThumbnailImageAbsoluteURI { get; set; }

        // user
        public int UserId { get; set; }
        public string Username { get; set; }
        public bool IsAccountConfirmed { get; set; }
    }

    public class ProductSearchStatisticsApiModel
    {
        public ProductSearchStatisticsApiModel()
        {
            this.CategoryTotals = new List<CategoryTotalsApiModel>();
            this.SubcategoryTotals = new List<SubcategoryTotalsApiModel>();
            this.SectionTotals = new List<SectionTotalsApiModel>();
        }

        public int TotalItemsFound { get; set; }
        public List<CategoryTotalsApiModel> CategoryTotals { get; set; }
        public List<SubcategoryTotalsApiModel> SubcategoryTotals { get; set; }
        public List<SectionTotalsApiModel> SectionTotals { get; set; }
    }

    public class SectionTotalsApiModel
    {
        public int? SectionId { get; set; }
        public string Name { get; set; }
        public int? TotalFound { get; set; }
        public int? SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public string CategoryName { get; set; }
        public int? CategoryId { get; set; }
    }

    public class SubcategoryTotalsApiModel
    {
        public int? TotalFound { get; set; }
        public int? SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public string CategoryName { get; set; }
        public int? CategoryId { get; set; }
    }

    public class CategoryTotalsApiModel
    {
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? TotalFound { get; set; }
    }
}