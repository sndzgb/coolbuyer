using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.Extensions
{
    /// <summary>
    /// DateTime formatters and helpers.
    /// </summary>
    public static class DateTimeFormatter
    {
        /// <summary>
        /// Converts the given unix time to UTC/ GMT instance.
        /// </summary>
        /// <param name="unixTime">Unix seconds passed.</param>
        /// <returns>DateTime representation expressed as UTC.</returns>
        public static DateTime FromUnixTime(this long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }
    }
}
