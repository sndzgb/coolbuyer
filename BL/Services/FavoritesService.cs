using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolBuyer.Server.BusinessLogic.Common.DTOs.ResponseModels;
using CoolBuyer.Server.BusinessLogic.Database;
using System.Data.Entity;
using System.Data.SqlClient;
using CoolBuyer.Server.BusinessLogic.Common.DTOs.Request.Write;
using CoolBuyer.Server.BusinessLogic.Common.Exceptions;

namespace CoolBuyer.Server.BusinessLogic.Services
{
    public class FavoritesService : IFavoritesService
    {
        private readonly ICurrentUserService currentUserService;
        private readonly ICoolBuyerDbContext dbContext;


        public FavoritesService(ICoolBuyerDbContext dbContext, ICurrentUserService currentUserService)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
        }


        public void Add(AddProductToFavoritesDTO model)
        {
            if (currentUserService == null)
            {
                throw new OperationNotAllowedException("Access denied.");
            }

            var product = dbContext.Products.Where(p => p.Id == model.ProductId).FirstOrDefault();

            if (product == null)
            {
                throw new NotFoundException($"Product with ID '{model.ProductId}' was not found.");
            }

            if (product.ExpirationDate < DateTime.UtcNow)
            {
                throw new OperationNotAllowedException("You cannot favorite inactive product.");
            }

            if (product.UserId == currentUserService.Id)
            {
                throw new OperationNotAllowedException("You cannot add your own product to favorites.");
            }

            var result = dbContext
                .Users
                .Include(
                    f => f.Favorites //.Select(x => x.ProductId == model.ProductId && x.UserId == currentUserService.Id)
                )
                .Where(u => u.Id == currentUserService.Id)
                .FirstOrDefault();

            var favorite = new Common.Models.FavoriteProductModel()
            {
                ProductId = model.ProductId,
                UserId = currentUserService.Id
            };

            var fav = result.Favorites.Where(x => x.ProductId == model.ProductId).FirstOrDefault();

            if (fav == null)
            {
                result.Favorites.Add(favorite);
            }
            else
            {
                // call 'Remove(productId)'
                result.Favorites.Remove(fav);
            }

            dbContext.Commit();
        }

        public void Delete(int productId)
        {
            Add(new AddProductToFavoritesDTO() { ProductId = productId });
        }

        public IEnumerable<UserFavoriteDTO> Get(int take = 5, int page = 1)
        {
            return dbContext
                .Database
                .SqlQuery<UserFavoriteDTO>("EXEC usp_GetFavoritesForUser @UserId, @Take, @Page",
                    new SqlParameter[] 
                    {
                        new SqlParameter("@UserId", currentUserService.Id),
                        new SqlParameter("@Take", take),
                        new SqlParameter("@Page", page)
                    }
                )
                .AsEnumerable();
        }
    }
}
