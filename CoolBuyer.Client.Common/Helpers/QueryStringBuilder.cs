using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.Helpers
{
    /// <summary>
    /// Builder used to convert values into query string parameters.
    /// </summary>
    public static class QueryStringBuilder
    {
        public static string Build<T>(T @object) where T : class
        {
            StringBuilder query = @object.ToQueryStringBuilder();

            if (query.Length > 0)
            {
                query[0] = '?';
            }

            return query.ToString();
        }

        private static StringBuilder ToQueryStringBuilder<T>(this T obj, string prefix = "") where T : class
        {
            var gatherer = new StringBuilder();

            foreach (var p in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (p.GetValue(obj, new object[0]) != null)
                {
                    var value = p.GetValue(obj, new object[0]);

                    if (p.PropertyType.IsArray && value.GetType() == typeof(DateTime[]))
                    {
                        foreach (var item in value as DateTime[])
                        {
                            gatherer.Append($"&{p.Name}={item.ToString("yyyy-MM-dd")}");
                        }
                    }
                    else if (p.PropertyType.IsArray)
                    {
                        foreach (var item in value as Array)
                        {
                            gatherer.Append($"&{p.Name}={item}");
                        }
                    }
                    else if (p.PropertyType == typeof(string))
                    {
                        gatherer.Append($"&{p.Name}={value}");
                    }
                    else if (p.PropertyType == typeof(DateTime) && !value.Equals(Activator.CreateInstance(p.PropertyType))) // is not default 
                    {
                        gatherer.Append($"&{p.Name}={((DateTime)value).ToString("yyyy-MM-dd")}");
                    }
                    else if (p.PropertyType.IsValueType && !value.Equals(Activator.CreateInstance(p.PropertyType))) // is not default 
                    {
                        gatherer.Append($"&{p.Name}={value}");
                    }
                    else if (p.PropertyType.IsClass)
                    {
                        gatherer.Append(value.ToQueryStringBuilder($"{p.Name}."));
                    }
                }
            }

            return gatherer;
        }
    }
}
