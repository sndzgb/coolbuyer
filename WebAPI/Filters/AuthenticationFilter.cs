using CoolBuyer.Server.Authentication.Managers;
using CoolBuyer.Server.BusinessLogic.Common.Managers;
using CoolBuyer.Server.BusinessLogic.Services;
using CoolBuyer.Server.WebApi.Security;
using CoolBuyer.Server.WebApi.Security.Principal;
using CoolBuyer.Server.WebApi.WebApiModels;
using CoolBuyer.Server.WebApi.WebApiModels.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace CoolBuyer.Server.WebApi.Filters
{
    public class AuthenticationFilter : AuthorizationFilterAttribute
    {
        private void SetPrincipal(HttpActionContext actionContext)
        {
            var requestScope = actionContext.Request.GetDependencyScope();
            var UserManager = requestScope.GetService(typeof(IUserManager)) as IUserManager;
            var TokenService = requestScope.GetService(typeof(ITokenGeneratorService)) as ITokenGeneratorService;

            CoolBuyerPrincipal principal = null;
            BusinessLogic.Common.DTOs.ApplicationUserDTO userDTO = null;
            bool isValid = true;
            AuthenticationHeaderValue authenticationHeader = GetAuthenticationHeader(actionContext);

            if (authenticationHeader != null)
            {
                if (authenticationHeader.Scheme.ToLower() == "bearer")
                {
                    var userClaimsPrincipal = TokenService.VerifyToken(authenticationHeader.Parameter);
                    if (userClaimsPrincipal.Identity != null)
                    {
                        userDTO = UserManager
                                    .FindUser(userClaimsPrincipal.Claims.Where(c => c.Type == "Id").Select(x => Int32.Parse(x.Value))
                                    .FirstOrDefault());

                        if (userDTO.IsLockedOut)
                        {
                            isValid = false;
                        }

                        principal = new CoolBuyerPrincipal(userDTO.Id, userDTO.Username, userDTO.Email);
                    }
                    else
                    {
                        isValid = false;
                    }
                }
                else
                {
                    isValid = false;
                }
            }
            else
            {
                isValid = false;
            }
            if (!isValid)
            {
                principal = new CoolBuyerPrincipal();
            }
            actionContext.RequestContext.Principal = principal;
            Thread.CurrentPrincipal = principal;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            SetPrincipal(actionContext);

            if (SkipAuthorization(actionContext))
            {
                return;
            }

            if (!actionContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                SetErrorResponse(actionContext);
                return;
            }
        }
        
        public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            return Task.Run(() => OnAuthorization(actionContext));
        }


        #region Helpers

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                   || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }

        private static void SetErrorResponse(HttpActionContext actionContext, string errorMessage = "")
        {
            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                ReasonPhrase = errorMessage.Length > 0 ? errorMessage : "Access Denied"
            };
        }

        private AuthenticationHeaderValue GetAuthenticationHeader(HttpActionContext actionContext)
        {
            var requestHeaders = actionContext.Request.Headers;
            var authenticationHeader = requestHeaders.Authorization;
            return authenticationHeader;
        }

        #endregion
    }
}