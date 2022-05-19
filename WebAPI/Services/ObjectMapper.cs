using CoolBuyer.Server.BusinessLogic.Common.Constants;
using CoolBuyer.Server.BusinessLogic.Common.DTOs.Request.Read;
using CoolBuyer.Server.BusinessLogic.Common.DTOs.ResponseModels;
using CoolBuyer.Server.WebApi.WebApiModels.Products;
using CoolBuyer.Server.WebApi.WebApiModels.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CoolBuyer.Server.WebApi.Services
{
    public class ObjectMapper : IMapper
    {
        public ObjectMapper()
        {
        }


        public TResult Map<TResult, TSource>(TSource source)
        {
            return default(TResult);
        }

        public TResult Map<TResult, TSource>(TSource source, object[] optionalParameters)
        {
            Type sourceType = typeof(TSource);

            if (sourceType == typeof(ProductDetailsDTO))
            {
                ProductDetailsDTO model = (ProductDetailsDTO)Convert.ChangeType(source, typeof(ProductDetailsDTO));
                var mapped = MapToProductDetailsApiModel(model, optionalParameters);
                return (TResult)(object)mapped;
            }

            if (sourceType == typeof(ProductSearchDTO))
            {
                ProductSearchDTO model = (ProductSearchDTO)Convert.ChangeType(source, typeof(ProductSearchDTO));
                var mapped = MapToProductSearchApiModel(model, optionalParameters);
                return (TResult)(object)mapped;
            }

            if (sourceType == typeof(IndexPageProductDetailsDTO))
            {
                IndexPageProductDetailsDTO model = (IndexPageProductDetailsDTO)Convert.ChangeType(source, typeof(IndexPageProductDetailsDTO));
                var mapped = MapToIndexPageProductDetailsApiModel(model, optionalParameters);
                return (TResult)(object)mapped;
            }

            if (sourceType == typeof(UserProfileIndexDetailsDTO))
            {
                UserProfileIndexDetailsDTO model = (UserProfileIndexDetailsDTO)Convert.ChangeType(source, typeof(UserProfileIndexDetailsDTO));
                var mapped = MapToUserProfileIndexDetailsApiModel(model, optionalParameters);
                return (TResult)(object)mapped;
            }

            return default(TResult);
        }


        private UserProfileIndexDetailsApiModel MapToUserProfileIndexDetailsApiModel(UserProfileIndexDetailsDTO model, object[] parameters)
        {
            string productImagesBasePath = (string)parameters[1];
            string profileImagesBasePath = (string)parameters[0];

            var result = new UserProfileIndexDetailsApiModel();

            if (result.UserProfile.ProfileThumbnailImageURI != null)
            {
                result.UserProfile.ProfileThumbnailImageURI = Path.Combine(
                    profileImagesBasePath,
                    model.UserProfile.ProfileThumbnailImageUrl
                );
            }
            
            result.UserProfile.ProfileThumbnailImageName = model.UserProfile.ProfileThumbnailImageName;
            result.UserProfile.RegistrationDate = model.UserProfile.RegistrationDate;
            result.UserProfile.TotalActiveProducts = model.UserProfile.TotalActiveProducts;
            result.UserProfile.UserId = model.UserProfile.UserId;
            result.UserProfile.Username = model.UserProfile.Username;

            foreach (var prod in model.Products)
            {
                result.Products.Add(new ProductIndexDetailsApiModel()
                {
                    ExpirationDate = prod.ExpirationDate,
                    PostedDate = prod.PostedDate,
                    Price = prod.Price,
                    ProductId = prod.ProductId,
                    ProductThumbnailImageName = prod.ProductThumbnailImageName,
                    ProductThumbnailImageURI = prod.ProductThumbnailImageUrl == null ? null : Path.Combine(productImagesBasePath, prod.ProductThumbnailImageUrl),
                    Title = prod.Title,
                    TotalComments = prod.TotalComments,
                    TotalDislikes = prod.TotalDislikes,
                    TotalLikes = prod.TotalLikes,
                    TotalTimesFavorited = prod.TotalTimesFavorited
                });
            }

            return result;
        }

        private IndexPageProductDetailsApiModel MapToIndexPageProductDetailsApiModel(IndexPageProductDetailsDTO model, object[] parameters)
        {
            string basePath = (string)parameters[0];
            var result = new IndexPageProductDetailsApiModel();

            foreach (var cat in model.Categories)
            {
                result.Categories.Add(new LatestProductsByCategoryApiModel()
                {
                    CategoryId = cat.CategoryId,
                    CategoryName = cat.CategoryName,
                    CategoryThumbnailURI = cat.CategoryThumbnailUrl == null ? null : Path.Combine(basePath, cat.CategoryThumbnailUrl),
                    Products = cat.Products.Select(x => new ProductInfoDetailsApiModel()
                    {
                        Description = x.Description,
                        ExpirationDate = x.ExpirationDate,
                        PostedDate = x.PostedDate,
                        Price = x.Price,
                        ProductId = x.ProductId,
                        ProductThumbnailName = x.ProductThumbnailName,
                        ProductThumbnailURI = x.ProductThumbnailUrl == null ? null : Path.Combine(basePath, x.ProductThumbnailUrl),
                        Title = x.Title
                    }).ToList()
                });
            }

            return result;
        }

        private ProductSearchApiModel MapToProductSearchApiModel(ProductSearchDTO model, object[] parameters)
        {
            var basePath = (string)parameters[0];
            var searchParameters = (ProductsSearchOptionsDTO)parameters[1];

            var details = new ProductSearchApiModel();

            details.Products = model.Products.Select(
                    x => new ProductSearchDetailsApiModel
                    {
                        CategoryId = x.CategoryId,
                        CategoryName = x.CategoryName,
                        Description = x.Description,
                        ExpirationDate = x.ExpirationDate,
                        IsAccountConfirmed = x.IsAccountConfirmed,
                        PostedDate = x.PostedDate,
                        Price = x.Price,
                        ProductId = x.ProductId,
                        ProductThumbnailImageAbsoluteURI = (x.ProductThumbnailImage == null ? null : Path.Combine(basePath, x.ProductThumbnailImage)),
                        SectionId = x.SectionId,
                        SectionName = x.SectionName,
                        SubcategoryId = x.SubcategoryId,
                        SubcategoryName = x.SubcategoryName,
                        Title = x.Title,
                        TotalComments = x.TotalComments,
                        TotalDislikes = x.TotalDislikes,
                        TotalLikes = x.TotalLikes,
                        UserId = x.UserId,
                        Username = x.Username
                    }
                ).ToList();
            
            details.SearchParameters.SearchString = searchParameters.SearchString;
            details.SearchParameters.CategoryId = searchParameters.CategoryId;
            details.SearchParameters.MaxPrice = searchParameters.MaxPrice;
            details.SearchParameters.MinPrice = searchParameters.MinPrice;
            details.SearchParameters.Page = searchParameters.Page;
            details.SearchParameters.SectionId = searchParameters.SectionId;
            details.SearchParameters.SortBy = searchParameters.SortBy;
            details.SearchParameters.SubcategoryId = searchParameters.SubcategoryId;
            details.SearchParameters.Take = searchParameters.Take;

            details.Stats.CategoryTotals = model.Stats.CategoryTotals.Select(
                    x => new CategoryTotalsApiModel
                    {
                        CategoryId = x.CategoryId,
                        CategoryName = x.CategoryName,
                        TotalFound = x.TotalFound
                    }
                ).ToList();

            details.Stats.SubcategoryTotals = model.Stats.SubcategoryTotals.Select(
                    x => new SubcategoryTotalsApiModel
                    {
                        TotalFound = x.TotalFound,
                        CategoryId = x.CategoryId,
                        CategoryName = x.CategoryName,
                        SubcategoryId = x.SubcategoryId,
                        SubcategoryName = x.SubcategoryName
                    }
                ).ToList();

            details.Stats.SectionTotals = model.Stats.SectionTotals.Select(
                    x => new SectionTotalsApiModel
                    {
                        CategoryId = x.CategoryId,
                        CategoryName = x.CategoryName,
                        Name = x.Name,
                        SectionId = x.SectionId,
                        SubcategoryId = x.SubcategoryId,
                        SubcategoryName = x.SubcategoryName,
                        TotalFound = x.TotalFound
                    }
                ).ToList();

            details.Stats.TotalItemsFound = model.Stats.TotalItemsFound;

            return details;
        }

        private ProductDetailsApiModel MapToProductDetailsApiModel(ProductDetailsDTO model, object[] parameters)
        {
            var details = new ProductDetailsApiModel();
            details.Product.CategoryId = model.Product.CategoryId;
            details.Product.CategoryName = model.Product.CategoryName;
            details.Product.Description = model.Product.Description;
            details.Product.ExpirationDate = model.Product.ExpirationDate;
            details.Product.IsFavorited = model.Product.IsFavorited;
            details.Product.LastUpdated = model.Product.LastUpdated;
            details.Product.LikeDislike = model.Product.LikeDislike;
            details.Product.PostedDate = model.Product.PostedDate;
            details.Product.PreferredCurrency = model.Product.PreferredCurrency;
            details.Product.Price = model.Product.Price;
            details.Product.ProductId = model.Product.ProductId;
            details.Product.SubcategoryId = model.Product.SubcategoryId;
            details.Product.SubcategoryName = model.Product.SubcategoryName;
            details.Product.Title = model.Product.Title;
            details.Product.TotalComments = model.Product.TotalComments;
            details.Product.TotalDislikes = model.Product.TotalDislikes;
            details.Product.TotalLikes = model.Product.TotalLikes;
            details.Product.TotalTimesFavorited = model.Product.TotalTimesFavorited;
            details.Product.UserId = model.Product.UserId;
            details.Product.Username = model.Product.Username;

            var basePath = (string)parameters[0];

            details.Images = model.Images.Select(i => new ProductImageApiModel()
            {
                Name = i.ImageName,
                Uri = Path.Combine(basePath, i.Url)
            }).ToList();

            return details;
        }
    }
}