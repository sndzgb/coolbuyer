using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolBuyer.Server.WebApi.Services
{
    public interface IMapper
    {
        /// <summary>
        /// Maps TSource to TResult instance.
        /// </summary>
        /// <typeparam name="TResult">Result of mapping operation.</typeparam>
        /// <typeparam name="TSource">Source model to map from.</typeparam>
        /// <param name="source">Populated model from which the values will be used.</param>
        /// <returns></returns>
        TResult Map<TResult, TSource>(TSource source);

        /// <summary>
        /// Maps TSource to TResult instance.
        /// </summary>
        /// <typeparam name="TResult">Result of mapping operation.</typeparam>
        /// <typeparam name="TSource">Source model to map from.</typeparam>
        /// <param name="source">Populated model from which the values will be used.</param>
        /// <param name="optionalParameters">Optional parameters to use with mapping. Order of the parameters matters.</param>
        /// <returns></returns>
        TResult Map<TResult, TSource>(TSource source, object[] optionalParameters);
    }
}