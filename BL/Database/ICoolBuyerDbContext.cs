using CoolBuyer.Server.BusinessLogic.Common.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Database
{
    public interface ICoolBuyerDbContext
    {
        DbSet<ProductModel> Products { get; set; }
        DbSet<UserModel> Users { get; set; }

        System.Data.Entity.Database Database { get; }

        void Commit();
        void CommitAsync(CancellationToken cancellationToken);
    }
}
