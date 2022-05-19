using CoolBuyer.Server.BusinessLogic.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Services
{
    public interface IExchangeRateService
    {
        decimal GetMultiplier(Currency destinationCurrency);
    }
}
