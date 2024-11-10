using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;


namespace CaseStudyMvc.Entities.Helper
{
    public static class Orm
    {
        public enum Conn
        {
            dbMusaKir = 1
        }

        public static string ConnectionString(Conn conn)
        {
            string ConnectionDb = System.Configuration.ConfigurationManager.AppSettings["MusaKirDb"];

            string ResultConnection = "";

            if (conn == Conn.dbMusaKir)
                ResultConnection = ConnectionDb;

            return ResultConnection;
        }

        public static object Execute(Conn type, string sql)
        {
            using (var connection = new SqlConnection(ConnectionString(type)))
            {
                return connection.Execute(sql);
            }
        }

        public static async Task<object> ExecuteAsync(Conn type, string sql)
        {
            using (var connection = new SqlConnection(ConnectionString(type)))
            {
                return await connection.ExecuteAsync(sql);
            }
        }

        public static object ExecuteScalar(Conn type, string sql)
        {
            using (var connection = new SqlConnection(ConnectionString(type)))
            {
                return connection.ExecuteScalar(sql);
            }
        }

        public static async Task<object> ExecuteScalarAsync(Conn type, string sql)
        {
            using (var connection = new SqlConnection(ConnectionString(type)))
            {
                return await connection.ExecuteScalarAsync<object>(sql);
            }
        }

        public static IEnumerable<T> Query<T>(Conn type, string sql)
        {
            using (var connection = new SqlConnection(ConnectionString(type)))
            {
                var result = connection.Query<T>(sql);

                return result.AsList();
            }
        }

        public static async Task<IEnumerable<T>> QueryAsync<T>(Conn type, string sql)
        {
            using (var connection = new SqlConnection(ConnectionString(type)))
            {
                var result = await connection.QueryAsync<T>(sql);

                return result.AsList();
            }
        }

        public static object QueryFirst(Conn type, string sql)
        {
            using (var connection = new SqlConnection(ConnectionString(type)))
            {
                return connection.QueryFirst(sql);
            }
        }

        public static async Task<T> QueryFirstAsync<T>(Conn type, string sql, DynamicParameters parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString(type)))
            {
                var result = await connection.QueryFirstAsync<T>(sql, parameters);

                return result;
            }
        }

        public static DataTable ConvertToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                dataTable.Columns.Add(prop.Name, type);
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

        public static object ExecuteReader(Conn type, string sql)
        {
            using (var connection = new SqlConnection(ConnectionString(type)))
            {
                return connection.ExecuteReader(sql);
            }
        }

    }
}
