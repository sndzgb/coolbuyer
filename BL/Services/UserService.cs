using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolBuyer.Server.BusinessLogic.Common.DTOs;
using CoolBuyer.Server.BusinessLogic.Common.Models;
using CoolBuyer.Server.BusinessLogic.Database;
using CoolBuyer.Server.BusinessLogic.Common.Extensions;
using System.Data.SqlClient;
using CoolBuyer.Server.BusinessLogic.Common.DTOs.ResponseModels;
using System.IO;

namespace CoolBuyer.Server.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly ICoolBuyerDbContext dbContext;


        public UserService(ICoolBuyerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        
        public UserProfileIndexDetailsDTO GetProfileIndexDetails(int userId, int take = 5, int page = 1)
        {
            var results = DbContextMultipleResultSet.MultipleResults(dbContext,
                    "usp_GetProfileIndexDetails @UserId, @Take, @Page",
                new SqlParameter[]
                {
                    new SqlParameter("@UserId", userId),
                    new SqlParameter("@Take", take),
                    new SqlParameter("@Page", page)
                })
            .With<ProductIndexDetailsDTO>()
            .With<UserProfileDetailsDTO>()
            .Execute();

            var profileDetails = new UserProfileIndexDetailsDTO();

            profileDetails.Products = results[0] as List<ProductIndexDetailsDTO>;
            profileDetails.UserProfile = results[1].Cast<UserProfileDetailsDTO>().First();

            foreach (var prod in profileDetails.Products)
            {
                if (prod.ProductThumbnailImageName != null)
                {
                    prod.ProductThumbnailImageUrl = Path.Combine(
                                                        MakeVariableUrlPartForImage(prod.PostedDate, prod.ProductId, prod.ProductThumbnailImageName)
                                                    );
                }
            }

            return profileDetails;
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

    }
}
