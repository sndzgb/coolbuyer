using Autofac;
using Autofac.Integration.Mvc;
using CoolBuyer.Client.Common.ApiClient;
using CoolBuyer.Client.Common.ApiEndpoints;
using CoolBuyer.Client.Common.ApiEndpoints.EndpointsAbstractions;
using CoolBuyer.Client.Common.Services;
using CoolBuyer.Client.Web.MVC.Controllers;
using CoolBuyer.Client.Web.MVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CoolBuyer.Client.Web.MVC.App_Start
{
    public class DIContainerBuilder
    {
        public IContainer Container { get; set; }

        public static void Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var asm = System.Reflection.Assembly.GetAssembly(typeof(MvcApplication));
            builder.RegisterControllers(asm).AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterType<AccountController>().InstancePerRequest();
            builder.RegisterType<ProductsController>().InstancePerRequest();
            builder.RegisterType<FavoritesController>().InstancePerRequest();
            builder.RegisterType<UsersController>().InstancePerRequest();
            builder.RegisterType<ErrorController>().InstancePerRequest();

            builder.RegisterType<ProductCommentsController>().InstancePerRequest();
            
            builder.RegisterType<AuthenticatedUserService>().As<IAuthenticatedUserService>().InstancePerRequest();

            builder.RegisterType<AccountEndpoint>().As<IAccountEndpoint>().InstancePerDependency();
            builder.RegisterType<ProductsEndpoint>().As<IProductsEndpoint>().InstancePerDependency();
            builder.RegisterType<FavoritesEndpoint>().As<IFavoritesEndpoint>().InstancePerDependency();
            builder.RegisterType<UsersEndpoint>().As<IUsersEndpoint>().InstancePerDependency();
            builder.RegisterType<ProductCommentsEndpoint>().As<IProductCommentsEndpoint>().InstancePerDependency();

            builder.RegisterType<CoolBuyerHttpClient>().As<ICoolBuyerHttpClient>().SingleInstance();
            builder.RegisterType<HttpClient>().AsSelf().SingleInstance();
            
            builder.RegisterFilterProvider();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}