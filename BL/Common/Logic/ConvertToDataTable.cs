﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.Logic
{
    public static class ConvertToDataTable
    {
        /// <summary>
        /// Converts specified columns and rows to a datatable. Note: order of columns and rows is important.
        /// </summary>
        /// <param name="columns">Names of columns to add to datatable.</param>
        /// <param name="rows">Values to insert into each row.</param>
        /// <returns>Populated datatable with specified values.</returns>
        public static DataTable Convert<T>(List<T> items)
        {
            DataTable dataTable = new DataTable();
            
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in Props)
            {
                dataTable.Columns.Add(prop.Name);
            }

            foreach (T item in items)
            {
                var values = new object[Props.Length];

                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }
}
