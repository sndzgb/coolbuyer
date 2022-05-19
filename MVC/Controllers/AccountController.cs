using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CoolBuyer.Client.Common.ApiEndpoints.EndpointsAbstractions;
using CoolBuyer.Client.Common.ClientModels.Account;
using CoolBuyer.Client.Web.MVC.Controllers.BaseController;
using System.Configuration;

namespace CoolBuyer.Client.Web.MVC.Controllers
{
    public class AccountController : BaseCoolBuyerController
    {
        private readonly IAccountEndpoint accountEndpoint;
        private string authenticationCookieName = ConfigurationManager.AppSettings["AuthenticationCookieName"];


        public AccountController(IAccountEndpoint accountEndpoint)
        {
            this.accountEndpoint = accountEndpoint;
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("login")]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserLoginViewModel model, string returnUrl)
        {
            var authenticatedUser = await accountEndpoint.LoginAsync(model);
            SetAuthenticationCookie(authenticatedUser);
            return RedirectToLocal(returnUrl);
        }
        
        private void SetAuthenticationCookie(AuthenticatedUserViewModel authenticatedUser)
        {
            // get domain from web.config
            HttpContext.Response.Cookies.Add(
                new HttpCookie(authenticationCookieName)
                {
                    Value = authenticatedUser.Token,
                    Domain = "localhost",
                    Expires = authenticatedUser.ExpirationDateUTC,
                    Path = "/"
                });
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("register")]
        public ActionResult Register(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegistrationDetailsViewModel model, string returnUrl)
        {
            await accountEndpoint.RegisterAsync(model);
            return RedirectToLocal(returnUrl);
        }

        // HttpPost?
        [HttpGet]
        [AllowAnonymous]
        [ActionName("confirm-account")]
        public async Task<ActionResult> ConfirmAccount(string token, int userId)
        {
            await accountEndpoint.ConfirmAccountAsync(token, userId);
            ViewBag.InfoMessage = "Your account has been confirmed successfully. You can now log in.";
            return RedirectToAction("login", "account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("logout")]
        public ActionResult Logout()
        {
            if (Request.Cookies[authenticationCookieName] != null)
            {
                var c = new HttpCookie(authenticationCookieName)
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(c);
            }

            return RedirectToAction("login", "account");
        }


        #region helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl) && returnUrl != "")
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("index", "products");
        }
        #endregion
    }
}