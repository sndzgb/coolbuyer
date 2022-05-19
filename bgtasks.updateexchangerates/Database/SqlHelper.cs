using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGTasks.UpdateExchangeRates.Database
{
    public static class SqlHelper
    {
        private static readonly string connString = ConfigurationManager.ConnectionStrings["CoolBuyerDbConnectionString"].ConnectionString;

        public static T ExecuteRaw<T>(string sql, IEnumerable<SqlParameter> parameters)
        {
            using (var connection = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sql, connection);
                if (parameters != null && parameters.Count() > 0)
                {
                    foreach (var item in parameters)
                    {
                        cmd.Parameters.AddWithValue(item.ParameterName, item.Value);
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                var serializedDT = JsonConvert.SerializeObject(dt);
                return JsonConvert.DeserializeObject<T>(serializedDT);
            }
        }

        public static void ExecuteStoredProcedure(string sql, SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parameters != null && parameters.Count() > 0)
                    {
                        foreach (var item in parameters)
                        {
                            cmd.Parameters.AddWithValue(item.ParameterName, item.Value);
                        }
                    }

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
