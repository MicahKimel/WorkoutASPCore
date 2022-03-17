using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace APIDataManager.Library.Internal.DataAccess
{
    internal class SqlDataAccess
    {
        private IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }
        public string GetConnectionString(string name)
        {
            return _config.GetConnectionString(name);
        }

        public bool UserExists<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection con = new SqlConnection(connectionString))
            {
                List<T> rows = con.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ToList();

                return true;//rows.First() != null;
            }
        }

        public bool CreateUser<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection con = new SqlConnection(connectionString))
            {
                con.Query(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

                return true;//rows.First() != null;
            }
        }

        public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection con = new SqlConnection(connectionString))
            {
                List<T> rows = con.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ToList();
                
                return rows;
            }
        }

        public int SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection con = new SqlConnection(connectionString))
            {
                return con.Query<int>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).First();
            }
        }
    }
}
