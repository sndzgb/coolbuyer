using CoolBuyer.Server.BusinessLogic.Common.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;

namespace CoolBuyer.Server.BusinessLogic.Database
{
    public class CoolBuyerDbContext : DbContext, ICoolBuyerDbContext
    {
        public CoolBuyerDbContext(string connectionString)
            : base(connectionString)
        {

        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ProductModel> Products { get; set; }


        public void Commit()
        {
            base.SaveChanges();
        }

        public void CommitAsync(CancellationToken cancellationToken)
        {
            base.SaveChangesAsync(cancellationToken);
        }
    }
}
