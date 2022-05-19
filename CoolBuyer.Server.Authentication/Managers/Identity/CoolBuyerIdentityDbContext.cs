using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.Authentication.Managers.Identity
{
    public class CoolBuyerIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole,
                                                        int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public CoolBuyerIdentityDbContext(string connectionString)
            : base(connectionString)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;

            Database.SetInitializer<CoolBuyerIdentityDbContext>(null);
        }

        //public static CoolBuyerIdentityDbContext Create()
        //{
        //    return new CoolBuyerIdentityDbContext();
        //}

        protected override void OnModelCreating(DbModelBuilder model)
        {
            base.OnModelCreating(model);

            model.Conventions.Remove<PluralizingTableNameConvention>();
            model.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            model.Entity<ApplicationUser>().ToTable("ApplicationUsers");
            model.Entity<ApplicationUser>().Property(u => u.Id).HasColumnName("Id");
            model.Entity<ApplicationUser>().Property(u => u.PasswordHash).HasMaxLength(500);
            model.Entity<ApplicationUser>().Property(u => u.SecurityStamp).HasMaxLength(500);
            model.Entity<ApplicationUser>().Property(u => u.PhoneNumber).HasMaxLength(50);
            model.Entity<ApplicationUser>().Property(u => u.DateRegistered).HasColumnType("datetime2");

            model.Entity<ApplicationRole>().ToTable("ApplicationRoles");

            model.Entity<ApplicationUserRole>().ToTable("ApplicationUsersRoles");

            model.Entity<ApplicationUserLogin>().ToTable("ApplicationUsersLogins");

            model.Entity<ApplicationUserClaim>().ToTable("ApplicationUsersClaims");
            model.Entity<ApplicationUserClaim>().Property(u => u.ClaimType).HasMaxLength(100);
            model.Entity<ApplicationUserClaim>().Property(u => u.ClaimValue).HasMaxLength(100);
        }
    }
}
