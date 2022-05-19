using CoolBuyer.Client.Web.MVC.Filters;
using System.Web;
using System.Web.Mvc;

namespace CoolBuyer.Client.Web.MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // remove this one
            filters.Add(new HandleErrorAttribute());

            filters.Add(new GlobalExceptionHandlerAttribute());
            filters.Add(new RequireHttpsAttribute());
            filters.Add(new AuthenticateRequestAttribute());
            filters.Add(new AsyncTimeoutAttribute(100000));
            filters.Add(new ValidateModelAttribute());
        }
    }
}
