using BGTasks.Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGTasks.UpdateProductCount
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SqlHelper.ConnectionString = ConfigurationManager.ConnectionStrings["CoolBuyerDbConnectionString"].ConnectionString;
                SqlHelper.ExecuteStoredProcedure("usp_UpdateTotalItemsByCatSubcatSect", null);
            }
            catch (Exception e)
            {
                // log
            }
        }
    }
}
