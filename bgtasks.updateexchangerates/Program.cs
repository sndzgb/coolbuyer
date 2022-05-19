using BGTasks.UpdateExchangeRates.Database;
using BGTasks.UpdateExchangeRates.ScheduledTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGTasks.UpdateExchangeRates
{
    class Program
    {
        static void Main(string[] args)
        {
            //var task = Task.Run(async () => await ExchangeRateUpdaterTask.ExecuteAsync());
            //task.Wait();
            
            // options: DB, json, call web api controller action method, 
            try
            {
                var task = Task.Run(async () => await ExchangeRateUpdaterTask.ExecuteAsync());
                task.Wait();

                //var service = new Services.ExchangeRateService();
                //ExchangeRateUpdaterTask.ExecuteAsync().GetAwaiter().GetResult();
                //Console.WriteLine(Math.Round(service.GetMultiplier(BusinessLogic.Common.Constants.Currency.hrk), 2));
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString() ?? ex.InnerException.ToString());
                // log
            }
        }
    }
}
