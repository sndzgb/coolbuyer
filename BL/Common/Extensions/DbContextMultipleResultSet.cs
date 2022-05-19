using CoolBuyer.Server.BusinessLogic.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.Extensions
{
    public static class DbContextMultipleResultSet
    {
        public static MultipleResultSetWrapper MultipleResults(this ICoolBuyerDbContext dbCtx, string storedProcedure, IEnumerable<SqlParameter> parameters = null)
        {
            return new MultipleResultSetWrapper(dbCtx, storedProcedure, parameters);
        }

        public class MultipleResultSetWrapper
        {
            public List<Func<IObjectContextAdapter, DbDataReader, IEnumerable>> Results;
            private readonly string _storedProcedure;
            private readonly IEnumerable<SqlParameter> _parameters;
            private readonly ICoolBuyerDbContext _dbCtx;

            public MultipleResultSetWrapper(ICoolBuyerDbContext dbCtx, string storedProcedure, IEnumerable<SqlParameter> parameters = null)
            {
                _dbCtx = dbCtx;
                _storedProcedure = storedProcedure;
                _parameters = parameters;
                Results = new List<Func<IObjectContextAdapter, DbDataReader, IEnumerable>>();
            }

            public MultipleResultSetWrapper With<TResult>()
            {
                Results.Add((adapter, reader) => adapter
                    .ObjectContext
                    .Translate<TResult>(reader)
                    .ToList());

                return this;
            }

            public List<IEnumerable> Execute()
            {
                var results = new List<IEnumerable>();
                using (var connection = _dbCtx.Database.Connection)
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    command.CommandText = "EXEC " + _storedProcedure;

                    if (_parameters?.Any() ?? false)
                    {
                        command.Parameters.AddRange(_parameters.ToArray());
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        var adapter = ((IObjectContextAdapter)_dbCtx);

                        foreach (var resultSet in Results)
                        {
                            results.Add(resultSet(adapter, reader));
                            reader.NextResult();
                        }
                    }

                    return results;
                }
            }
        }
    }
}
