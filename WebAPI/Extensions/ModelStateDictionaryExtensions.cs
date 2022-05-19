using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace CoolBuyer.Server.WebApi.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        /// <summary>
        /// Returns model errors; key is property name, value is property error.
        /// </summary>
        /// <param name="modelState">ModelStateDictionary from web api controller context.</param>
        /// <returns></returns>
        public static Dictionary<string, object> GetModelErrors(this ModelStateDictionary modelState)
        {
            Dictionary<string, object> Errors = new Dictionary<string, object>();

            var error = modelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors });
            foreach (var err in error)
            {
                var lastDotIndex = err.Key.LastIndexOf('.');
                var key = err.Key.Remove(0, lastDotIndex + 1);
                Errors.Add(Char.ToLower(key[0]) + key.Substring(1), err.Errors[0].ErrorMessage);
            }

            return Errors;
        }
    }
}