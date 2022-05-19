using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolBuyer.Server.BusinessLogic.Common.DTOs;
using CoolBuyer.Server.BusinessLogic.Common.Models;
using CoolBuyer.Server.BusinessLogic.Database;
using System.Data.Entity;
using System.Data.SqlClient;
using CoolBuyer.Server.BusinessLogic.Common.Extensions;
using CoolBuyer.Server.BusinessLogic.Common.DTOs.Request.Read;
using CoolBuyer.Server.BusinessLogic.Common.DTOs.Request.Write;
using CoolBuyer.Server.BusinessLogic.Common.DTOs.ResponseModels;
using CoolBuyer.Server.BusinessLogic.Common.Exceptions;
using CoolBuyer.Server.BusinessLogic.Common.Logic;
using System.IO;
using CoolBuyer.Server.BusinessLogic.Common.Constants;
using System.Data;
using System.Data.SqlTypes;
using System.Transactions;

namespace CoolBuyer.Server.BusinessLogic.Services
{
    public class ProductService : IProductService
    {
        private readonly ICoolBuyerDbContext dbContext;
        private readonly ICurrentUserService currentUserService;
        private readonly IExchangeRateService exchangeRateService;
        private readonly IUserUploadsPathsService userUploadsPathsService;

        public ProductService(ICoolBuyerDbContext dbContext, 
            ICurrentUserService currentUserService,
            IExchangeRateService exchangeRateService,
            IUserUploadsPathsService userUploadsPathsService)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }

            if (currentUserService == null)
            {
                throw new ArgumentNullException("currentUserService");
            }

            if (exchangeRateService == null)
            {
                throw new ArgumentNullException("exchangeRateService");
            }

            if (userUploadsPathsService == null)
            {
                throw new ArgumentNullException("userUploadsPathsService");
            }

            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
            this.exchangeRateService = exchangeRateService;
            this.userUploadsPathsService = userUploadsPathsService;
        }


        public void Create(NewProductDTO product)
        {
            try
            {
                DateTime currentDateStamp = DateTime.UtcNow;

                //var model = MapToBusinessObject(...);
                var newProduct = new ProductModel()
                {
                    Price = ConvertToBaseCurrency(product.Price, product.SelectedCurrency),
                    Title = product.Title,
                    Description = product.Description,
                    CategoryId = product.CategoryId,
                    SubcategoryId = product.SubcategoryId,
                    SectionId = product.SectionId,
                    UserId = currentUserService.Id,
                    Images = product.Images.Select(x => new ProductImage()
                    {
                        ImageName = PrependTimeStamp(currentDateStamp, x.Name)
                    }).ToList()
                };

                ModelValidator.Validate(newProduct);

                var user = dbContext
                    .Users
                    .Include(a => a.AccountType)
                    .Where(x => x.Id == currentUserService.Id)
                    .FirstOrDefault();

                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                        var productIdOUT = new SqlParameter();
                        productIdOUT.ParameterName = "@ProductId";
                        productIdOUT.SqlDbType = SqlDbType.Int;
                        productIdOUT.Direction = ParameterDirection.Output;

                        var imgs = newProduct.Images.Select(x => new { x.ImageName }).ToList();
                        var imgsDT = ConvertToDataTable.Convert(imgs);
                        var expDays = GetProductExpirationDaysForUser(user.AccountType.Name);

                        dbContext.Database.ExecuteSqlCommand($@"
                                EXEC usp_InsertProduct @UserId, @Title, @Description, @Price, 
                                    @CategoryId, @SubcategoryId, @SectionId, @PostedDate, @ExpirationDays, @ImagesTable, @ProductId OUT",
                                new SqlParameter[]
                                {
                                new SqlParameter("@UserId", currentUserService.Id),
                                new SqlParameter("@Title", newProduct.Title),
                                new SqlParameter("@Description", newProduct.Description),
                                new SqlParameter("@Price", newProduct.Price),
                                new SqlParameter("@CategoryId", newProduct.CategoryId),
                                new SqlParameter("@SubcategoryId", newProduct.SubcategoryId),
                                new SqlParameter("@SectionId", (newProduct.SectionId == 0 || newProduct.SectionId == null) ? SqlInt32.Null : (int)newProduct.SectionId),
                                new SqlParameter("@PostedDate", currentDateStamp),
                                new SqlParameter("@ExpirationDays", expDays
                                ),
                                new SqlParameter("@ImagesTable", SqlDbType.Structured) {
                                    TypeName = "UT_Images", Value = imgsDT
                                },
                                productIdOUT
                                }
                            );

                        foreach (var i in product.Images)
                        {
                            FileManager.Save(GetCompleteUploadPath(currentDateStamp, (int)productIdOUT.Value, i.Name), i.Stream);
                        }
                    }
                    catch (Exception e)
                    {
                        scope.Dispose();
                        throw;
                    }

                    scope.Complete();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void Update(UpdateProductDTO updatedProduct)
        {
            var product = dbContext
                .Products
                .Include(u => u.User)
                .Include(i => i.User.AccountType)
                .Where(p => p.Id == updatedProduct.ProductId)
                .FirstOrDefault();

            if (product == null)
            {
                throw new NotFoundException("Product not found.");
            }

            if (product.User.Id != currentUserService.Id)
            {
                throw new UnauthorizedException("Access denied.");
            }
            
            product.Price = updatedProduct.Price;
            product.Title = updatedProduct.Title;
            product.Description = updatedProduct.Description;

            ModelValidator.Validate(product);
            product.ExpirationDate = DateTime.UtcNow.AddDays(
                    GetProductExpirationDaysForUser(product.User.AccountType.Name)
                );
            product.LastUpdated = DateTime.UtcNow;
        }
        
        public void Delete(int productId)
        {
            var product = dbContext
                .Products
                .Include(u => u.User)
                .Where(p => p.Id == productId)
                .FirstOrDefault();

            if (product == null)
            {
                throw new NotFoundException("Product not found.");
            }

            if (product.User.Id != currentUserService.Id)
            {
                throw new OperationNotAllowedException("Access denied.");
            }

            dbContext.Database.ExecuteSqlCommand("EXEC usp_DeleteProduct @ProductId, @UserId",
                    new SqlParameter[]
                    {
                        new SqlParameter("@ProductId", productId),
                        new SqlParameter("@UserId", currentUserService.Id)
                    }
                );
        }

        public IndexPageProductDetailsDTO GetIndexPageProducts()
        {
            var queryResults = dbContext.Database.SqlQuery<ProductIndexPageDetailsBasicInfoDTO>("EXEC usp_GetIndexPageProducts").ToList();
            List<int> categories = queryResults.Select(x => x.CategoryId).Distinct().ToList();

            var results = new IndexPageProductDetailsDTO();

            foreach (var c in categories)
            {
                results.Categories.Add(new LatestProductsByCategoryDTO()
                {
                    CategoryId = c,
                    CategoryName = queryResults.Where(y => y.CategoryId == c).Select(x => x.CategoryName).FirstOrDefault(),
                    CategoryThumbnailUrl = queryResults.Where(y => y.CategoryId == c).Select(x => x.CategoryThumbnailUrl).FirstOrDefault(),
                    Products = queryResults.Where(x => x.CategoryId == c).Select(y => new ProductInfoDetailsDTO()
                    {
                        ProductId = y.ProductId,
                        ExpirationDate = y.ExpirationDate,
                        Description = y.Description,
                        PostedDate = y.PostedDate,
                        Price = y.Price,
                        ProductThumbnailName = y.ProductThumbnailName,
                        ProductThumbnailUrl = y.ProductThumbnailName == null ? null : 
                                MakeVariableUrlPartForImage((DateTime)y.ExpirationDate, (int)y.ProductId, y.ProductThumbnailName),
                        Title = y.Title
                    }).ToList()
                });
            }

            return results;
        }

        public void LikeDislike(LikeDislikeProductDTO model)
        {
            dbContext.Database.ExecuteSqlCommand(
                    "EXEC usp_LikeDislikeRemoveProductReaction @UserId, @ProductId, @LikeDislike",
                    new SqlParameter[] 
                    {
                        new SqlParameter("@UserId", currentUserService.Id),
                        new SqlParameter("@ProductId", model.ProductId),
                        new SqlParameter("@LikeDislike", model.LikeDislike)
                    }
                );
        }

        public ProductDetailsDTO GetSingleProductDetails(int productId)
        {
            var results = dbContext.MultipleResults("usp_GetProductDetails @ProductId, @UserId",
                new SqlParameter[] {
                    new SqlParameter("@ProductId", productId),
                    new SqlParameter("@UserId", currentUserService.Id == -1 ? SqlInt32.Null : currentUserService.Id)
                })
                .With<ProductInfoDTO>()
                .With<ProductImageDetailsDTO>()
                .Execute();

            var details = results[0].Cast<ProductInfoDTO>().First();
            var images = results[1] as List<ProductImageDetailsDTO>;

            var productDetailsDTO = new ProductDetailsDTO();
            productDetailsDTO.Product = details;
            productDetailsDTO.Images = images.Select(x => new ProductImageDTO
            {
                ImageName = x.ImageName,
                ProductId = x.ProductId,
                Url = MakeVariableUrlPartForImage(
                            productDetailsDTO.Product.PostedDate,
                            productDetailsDTO.Product.ProductId,
                            x.ImageName
                        )
            }).ToList();

            return productDetailsDTO;
        }

        public ProductSearchDTO SearchProducts(ProductsSearchOptionsDTO searchOptions)
        {
            var result = dbContext.MultipleResults(@"usp_SearchProducts
                @SearchString, @Take, @Page, @SortBy, @MinPrice,
                @MaxPrice, @CategoryId, @SubcategoryId, @SectionId
            ",
            new SqlParameter[]
            {
                new SqlParameter("@SearchString", searchOptions.SearchString),
                new SqlParameter("@Take", searchOptions.Take > 20 ? 10 : searchOptions.Take),
                new SqlParameter("@Page", searchOptions.Page),
                new SqlParameter("@SortBy", searchOptions.SortBy ?? SqlString.Null),
                new SqlParameter("@MinPrice", searchOptions.MinPrice ?? SqlInt32.Null),
                new SqlParameter("@MaxPrice", searchOptions.MaxPrice ?? SqlInt32.Null),
                new SqlParameter("@CategoryId", searchOptions.CategoryId ?? SqlInt32.Null),
                new SqlParameter("@SubcategoryId", searchOptions.SubcategoryId ?? SqlInt32.Null),
                new SqlParameter("@SectionId", searchOptions.SectionId ?? SqlInt32.Null),
            })
            .With<ProductSearchDetailsDTO>()
            .With<int>()
            .With<SectionTotalsDTO>()
            .With<SubcategoryTotalsDTO>()
            .With<CategoryTotalsDTO>()
            .Execute();

            var products = result[0] as List<ProductSearchDetailsDTO>;
            var totalItemsFound = result[1].Cast<int>().FirstOrDefault();
            var sectionStats = result[2] as List<SectionTotalsDTO>;
            var subcategoryStats = result[3] as List<SubcategoryTotalsDTO>;
            var categoryStats = result[4] as List<CategoryTotalsDTO>;

            var res = new ProductSearchDTO();
            res.Products = products;
            res.Stats.TotalItemsFound = totalItemsFound;
            res.Stats.SectionTotals = sectionStats;
            res.Stats.SubcategoryTotals = subcategoryStats;
            res.Stats.CategoryTotals = categoryStats;

            return res;
        }






        private List<string> PrepareImagesForSaving(DateTime date, List<string> imageNames)
        {
            if (date == null)
            {
                throw new ArgumentNullException("date");
            }

            if (imageNames == null)
            {
                throw new ArgumentNullException("imageNames");
            }

            var images = imageNames.Select(x => string.Concat(date.ToString("yyyyMMddHHmmss"), "_", x)).ToList();
            return images;
        }

        private static string MakeVariableUrlPartForImage(DateTime postedDate, int productId, string imageName)
        {
            var basePath = "";

            var day = postedDate.Day;
            var month = postedDate.Month;
            var year = postedDate.Year;

            return Path.Combine(
                    basePath,
                    year.ToString(),
                    month.ToString(),
                    day.ToString(),
                    productId.ToString(),
                    imageName
                );
        }
        
        /// <summary>
        /// Gets the complete path including disk drive letter.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="productId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetCompleteUploadPath(DateTime date, int productId, string fileName)
        {
            var imageUploadsBasePath = userUploadsPathsService.GetProductImagesStaticPathPart(true);
            return Path.Combine(
                    imageUploadsBasePath,
                    date.Year.ToString(), date.Month.ToString(), date.Day.ToString(),
                    productId.ToString(),
                    PrependTimeStamp(date, fileName)
                );
        }

        private static int GetProductExpirationDaysForUser(string accountTypeName)
        {
            switch (accountTypeName.ToLower())
            {
                case "basic":
                    return (int)ItemExpirationDaysForAccountType.Basic;
                case "advanced":
                    return (int)ItemExpirationDaysForAccountType.Advanced;
                case "elite":
                    return (int)ItemExpirationDaysForAccountType.Elite;
                case "platinum":
                    return (int)ItemExpirationDaysForAccountType.Platinum;
                default:
                    return 30;
            }
        }


        #region helpers


        private static string PrependTimeStamp(DateTime date, string item)
        {
            return string.Concat(date.ToString("yyyyMMddHHmmss"), "_", item);
        }

        private decimal ConvertToBaseCurrency(decimal price, string inputCurrency)
        {
            var multiplier = GetCurrencyMultiplier(inputCurrency);
            return price * multiplier;
        }

        private decimal GetCurrencyMultiplier(string currency)
        {
            Currency result;
            if (!Enum.TryParse(currency, out result))
            {
                result = Currency.EUR;
            }
            var multiplier = exchangeRateService.GetMultiplier(result);
            return multiplier;
        }

        private decimal ConvertFromBaseCurrency(decimal price, string preferredCurrency)
        {
            var multiplier = GetCurrencyMultiplier(preferredCurrency);
            return price * multiplier;
        }
        

        #endregion

        
    }
}
