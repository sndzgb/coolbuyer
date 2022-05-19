using BGTasks.UpdateExchangeRates.Database;
using BGTasks.UpdateExchangeRates.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BGTasks.UpdateExchangeRates.ScheduledTasks
{
    internal static class ExchangeRateUpdaterTask
    {
        private static HttpClient httpClient;


        static ExchangeRateUpdaterTask()
        {
            httpClient = new HttpClient();
        }


        public static async Task ExecuteAsync()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            using (var request = new HttpRequestMessage(HttpMethod.Get, "https://api.frankfurter.app/latest"))
            {
                using (var response = await httpClient.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var model = JsonConvert.DeserializeObject<ExchangeRateDetails>(content);

                        SqlHelper.ExecuteStoredProcedure(@"usp_UpdateExchangeRates",
                            new SqlParameter[]
                            {
                                new SqlParameter("@AUD", model.Rates.AUD),
                                new SqlParameter("@BGN", model.Rates.BGN),
                                new SqlParameter("@BRL", model.Rates.BRL),
                                new SqlParameter("@CAD", model.Rates.CAD),
                                new SqlParameter("@CHF", model.Rates.CHF),
                                new SqlParameter("@CNY", model.Rates.CNY),
                                new SqlParameter("@CZK", model.Rates.CZK),
                                new SqlParameter("@DKK", model.Rates.DKK),
                                new SqlParameter("@GBP", model.Rates.GBP),
                                new SqlParameter("@HKD", model.Rates.HKD),
                                new SqlParameter("@HRK", model.Rates.HRK),
                                new SqlParameter("@HUF", model.Rates.HUF),
                                new SqlParameter("@IDR", model.Rates.IDR),
                                new SqlParameter("@ILS", model.Rates.ILS),
                                new SqlParameter("@INR", model.Rates.INR),
                                new SqlParameter("@ISK", model.Rates.ISK),
                                new SqlParameter("@JPY", model.Rates.JPY),
                                new SqlParameter("@KRW", model.Rates.KRW),
                                new SqlParameter("@MXN", model.Rates.MXN),
                                new SqlParameter("@MYR", model.Rates.MYR),
                                new SqlParameter("@NOK", model.Rates.NOK),
                                new SqlParameter("@NZD", model.Rates.NZD),
                                new SqlParameter("@PHP", model.Rates.PHP),
                                new SqlParameter("@PLN", model.Rates.PLN),
                                new SqlParameter("@RON", model.Rates.RON),
                                new SqlParameter("@SEK", model.Rates.SEK),
                                new SqlParameter("@SGD", model.Rates.SGD),
                                new SqlParameter("@THB", model.Rates.THB),
                                new SqlParameter("@TRY", model.Rates.TRY),
                                new SqlParameter("@USD", model.Rates.USD),
                                new SqlParameter("@ZAR", model.Rates.ZAR)
                            });
                    }
                    else
                    {
                        throw new Exception(response.StatusCode + " - " + response.ReasonPhrase);
                    }
                }
            }
        }
    }
}
