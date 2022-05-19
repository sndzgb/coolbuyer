using CoolBuyer.Server.BusinessLogic.Common.Exceptions;
using CoolBuyer.Server.WebApi.WebApiModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace CoolBuyer.Server.WebApi.Filters
{
    public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exceptionType = actionExecutedContext.Exception.GetType();
            
            if (exceptionType == typeof(AccountConfirmationFailedException))
            {
                var ex = actionExecutedContext.Exception as AccountConfirmationFailedException;
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ErrorResponseWebApiModel()
                    {
                        ErrorMessage = ex.Message ?? "Something went wrong with your account confirmation. Please try again.",
                        StatusCode = 400,
                        Errors = null
                    }))
                };
            }
            else if (exceptionType == typeof(ModelValidationException))
            {
                var ex = actionExecutedContext.Exception as ModelValidationException;
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ErrorResponseWebApiModel()
                    {
                        ErrorMessage = ex.Message ?? "One or more errors occured while processing your request.",
                        StatusCode = 400,
                        Errors = null
                    }))
                };
            }
            else if (exceptionType == typeof(UnauthorizedException))
            {
                var ex = actionExecutedContext.Exception as UnauthorizedException;
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ErrorResponseWebApiModel()
                    {
                        ErrorMessage = ex.Message ?? "Access denied.",
                        StatusCode = 403,
                        Errors = null
                    }))
                };
            }
            else if (exceptionType == typeof(NotFoundException))
            {
                try
                {
                    var ex = actionExecutedContext.Exception as NotFoundException;

                    var response = new HttpResponseMessage(HttpStatusCode.NotFound);
                    response.Content = new StringContent(JsonConvert.SerializeObject(new ErrorResponseWebApiModel()
                    {
                        ErrorMessage = ex.Message ?? "The requested resource was not found.",
                        StatusCode = 404,
                        Errors = null
                    }), Encoding.UTF8, "application/json");

                    actionExecutedContext.Response = response;
                }
                catch (Exception e)
                {

                    throw;
                }
            }
            else if (exceptionType == typeof(OperationNotAllowedException))
            {
                var ex = actionExecutedContext.Exception as OperationNotAllowedException;
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ErrorResponseWebApiModel()
                    {
                        ErrorMessage = ex.Message,
                        StatusCode = 400,
                        Errors = null
                    }))
                };
            }
            else if (exceptionType == typeof(CreateNewUserException))
            {
                var ex = actionExecutedContext.Exception as CreateNewUserException;
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ErrorResponseWebApiModel()
                    {
                        ErrorMessage = ex.Message ?? "There was an error while creating a new account.",
                        StatusCode = 400,
                        Errors = null
                    }))
                };
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                var ex = actionExecutedContext.Exception as NotImplementedException;
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new ErrorResponseWebApiModel()
                    {
                        ErrorMessage = ex.Message ?? "There was an error on server.",
                        StatusCode = 500,
                        Errors = null
                    }))
                };
            }
            else
            {
                // do nothing; let it bubble; 'ExceptionHandler' will catch it & log it
            }
        }
    }
}