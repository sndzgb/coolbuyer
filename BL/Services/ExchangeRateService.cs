using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolBuyer.Server.BusinessLogic.Common.Constants;
using CoolBuyer.Server.BusinessLogic.Database;
using CoolBuyer.Server.BusinessLogic.Common.Models;

namespace CoolBuyer.Server.BusinessLogic.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly ICoolBuyerDbContext dbContext;


        public ExchangeRateService(ICoolBuyerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public decimal GetMultiplier(Currency destinationCurrency)
        {
            var model = dbContext
                .Database
                .SqlQuery<ExchangeRate>(@"SELECT e.Name,e.Value FROM ExchangeRates AS e")
                .FirstOrDefault();

            if (destinationCurrency == Currency.EUR)
            {
                switch (destinationCurrency)
                {
                    case Currency.AUD:
                        return model.AUD;
                    case Currency.BGN:
                        return model.BGN;
                    case Currency.BRL:
                        return model.BRL;
                    case Currency.CAD:
                        return model.CAD;
                    case Currency.CHF:
                        return model.CHF;
                    case Currency.CNY:
                        return model.CNY;
                    case Currency.CZK:
                        return model.CZK;
                    case Currency.DKK:
                        return model.DKK;
                    case Currency.GBP:
                        return model.GBP;
                    case Currency.HKD:
                        return model.HKD;
                    case Currency.HRK:
                        return model.HRK;
                    case Currency.HUF:
                        return model.HUF;
                    case Currency.IDR:
                        return model.IDR;
                    case Currency.ILS:
                        return model.ILS;
                    case Currency.INR:
                        return model.INR;
                    case Currency.ISK:
                        return model.ISK;
                    case Currency.JPY:
                        return model.JPY;
                    case Currency.KRW:
                        return model.KRW;
                    case Currency.MXN:
                        return model.MXN;
                    case Currency.MYR:
                        return model.MYR;
                    case Currency.NOK:
                        return model.NOK;
                    case Currency.NZD:
                        return model.NZD;
                    case Currency.PHP:
                        return model.PHP;
                    case Currency.PLN:
                        return model.PLN;
                    case Currency.RON:
                        return model.RON;
                    case Currency.SEK:
                        return model.SEK;
                    case Currency.SGD:
                        return model.SGD;
                    case Currency.THB:
                        return model.THB;
                    case Currency.TRY:
                        return model.TRY;
                    case Currency.USD:
                        return model.USD;
                    case Currency.ZAR:
                        return model.ZAR;
                    default:
                        return model.EUR;
                }
            }
            else
            {
                switch (destinationCurrency)
                {
                    case Currency.AUD:
                        return (model.EUR / model.AUD);
                    case Currency.BGN:
                        return (model.EUR / model.BGN);
                    case Currency.BRL:               
                        return (model.EUR / model.BRL);
                    case Currency.CAD:               
                        return (model.EUR / model.CAD);
                    case Currency.CHF:               
                        return (model.EUR / model.CHF);
                    case Currency.CNY:               
                        return (model.EUR / model.CNY);
                    case Currency.CZK:               
                        return (model.EUR / model.CZK);
                    case Currency.DKK:               
                        return (model.EUR / model.DKK);
                    case Currency.GBP:               
                        return (model.EUR / model.GBP);
                    case Currency.HKD:               
                        return (model.EUR / model.HKD);
                    case Currency.HRK:               
                        return (model.EUR / model.HRK);
                    case Currency.HUF:               
                        return (model.EUR / model.HUF);
                    case Currency.IDR:               
                        return (model.EUR / model.IDR);
                    case Currency.ILS:               
                        return (model.EUR / model.ILS);
                    case Currency.INR:               
                        return (model.EUR / model.INR);
                    case Currency.ISK:               
                        return (model.EUR / model.ISK);
                    case Currency.JPY:               
                        return (model.EUR / model.JPY);
                    case Currency.KRW:               
                        return (model.EUR / model.KRW);
                    case Currency.MXN:               
                        return (model.EUR / model.MXN);
                    case Currency.MYR:               
                        return (model.EUR / model.MYR);
                    case Currency.NOK:               
                        return (model.EUR / model.NOK);
                    case Currency.NZD:               
                        return (model.EUR / model.NZD);
                    case Currency.PHP:               
                        return (model.EUR / model.PHP);
                    case Currency.PLN:               
                        return (model.EUR / model.PLN);
                    case Currency.RON:               
                        return (model.EUR / model.RON);
                    case Currency.SEK:               
                        return (model.EUR / model.SEK);
                    case Currency.SGD:               
                        return (model.EUR / model.SGD);
                    case Currency.THB:               
                        return (model.EUR / model.THB);
                    case Currency.TRY:               
                        return (model.EUR / model.TRY);
                    case Currency.USD:               
                        return (model.EUR / model.USD);
                    case Currency.ZAR:               
                        return (model.EUR / model.ZAR);
                    default:
                        return model.EUR;
                }
            }
        }
    }
}
