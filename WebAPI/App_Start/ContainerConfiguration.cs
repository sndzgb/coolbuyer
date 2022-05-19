using Autofac;
using Autofac.Integration.WebApi;
using CoolBuyer.Server.Authentication.Managers.Identity;
using CoolBuyer.Server.Authentication.Services;
using CoolBuyer.Server.BusinessLogic.Common.Managers;
using CoolBuyer.Server.BusinessLogic.Common.Security.DataProtection;
using CoolBuyer.Server.BusinessLogic.Database;
using CoolBuyer.Server.BusinessLogic.Services;
using CoolBuyer.Server.WebApi.Controllers;
using CoolBuyer.Server.WebApi.Security.Principal;
using CoolBuyer.Server.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace CoolBuyer.Server.WebApi.App_Start
{
    public class ContainerConfiguration
    {
        public static IContainer Container { get; set; }


        public static void Configure()
        {
            var builder = new ContainerBuilder();

            var config = GlobalConfiguration.Configuration;

            var appSettings = ConfigurationManager.AppSettings;

            string connectionString = ConfigurationManager.ConnectionStrings["CoolBuyerDbConnectionString"].ConnectionString;
            builder.Register(
                    c => new CoolBuyerDbContext(connectionString)
                ).As<ICoolBuyerDbContext>().InstancePerDependency();
            builder.Register(
                    c => new CoolBuyerIdentityDbContext(connectionString)
                ).AsSelf().InstancePerDependency();


            var productImagesDirStaticPath = appSettings["ProductImagesBaseDirectory"];
            var profileImagesDirStaticPath = appSettings["ProfileImagesBaseDirectory"];
            var uploadsRootDir = appSettings["UploadsRootDirectory"];
            builder.Register(c => new UploadPathsService(uploadsRootDir, productImagesDirStaticPath, profileImagesDirStaticPath))
                .As<IUserUploadsPathsService>()
                .SingleInstance();

            builder.RegisterType<ProductsController>().InstancePerRequest();
            builder.RegisterType<UsersController>().InstancePerRequest();
            builder.RegisterType<AccountController>().InstancePerRequest();
            builder.RegisterType<ProductCommentsController>().InstancePerRequest();
            builder.RegisterType<FavoritesController>().InstancePerRequest();
            builder.RegisterType<UploadsController>().InstancePerRequest();


            builder.RegisterType<CoolBuyerPrincipal>().As<ICoolBuyerPrincipal>().InstancePerDependency();

            builder.RegisterType<ProductService>().As<IProductService>().InstancePerDependency();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerDependency();
            builder.RegisterType<AccountService>().As<IAccountService>().InstancePerDependency();
            builder.RegisterType<EmailMessagingService>().As<IEmailService>().InstancePerDependency();
            builder.RegisterType<ProductCommentService>().As<IProductCommentService>().InstancePerDependency();
            builder.RegisterType<FavoritesService>().As<IFavoritesService>().InstancePerDependency();
            builder.RegisterType<ExchangeRateService>().As<IExchangeRateService>().InstancePerDependency();

            builder.RegisterType<ApplicationRoleStore>().AsSelf().InstancePerDependency();
            builder.Register(c => new ApplicationUserStore(c.Resolve<CoolBuyerIdentityDbContext>())).AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterType<AesEncryption>().As<IDataProtector>();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();


            builder.RegisterType<AesCSPSymmetricEncryptionService>().As<ISymmetricCryptographyService>().InstancePerDependency();
            builder.RegisterType<JwtGeneratorService>().As<ITokenGeneratorService>().InstancePerDependency();


            builder.RegisterType<CurrentUserService>().As<ICurrentUserService>().InstancePerDependency();


            builder.RegisterType<ApplicationUserManager>().As<IUserManager>().InstancePerDependency();

            builder.RegisterType<ObjectMapper>().As<IMapper>().SingleInstance();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}