using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Client.Common.ApiClient
{
    public interface ICoolBuyerHttpClient
    {
        HttpClient ApiClient { get; }
    }
}
