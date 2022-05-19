using CoolBuyer.Server.BusinessLogic.Common.DTOs.ResponseModels;
using CoolBuyer.Server.BusinessLogic.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Services
{
    public interface IUserService
    {
        UserProfileIndexDetailsDTO GetProfileIndexDetails(int userId, int take = 5, int page = 1);
        //void Create(UserModel user);
    }
}
