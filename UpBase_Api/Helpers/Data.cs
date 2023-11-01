using Dapper;
using MySql.Data.MySqlClient;

namespace UpBase_Api.Helpers
{
    public class Data
    {
        private readonly string _connectionString;
        public Data(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("UpBase");
        }
        protected MySqlConnection SQLConnection()
        {
            using (MySqlConnection con = new MySqlConnection(_connectionString))
            {
                return con;
            }
        }
        protected int SQLExecute(string sql, object parameters = null)
        {
            return SQLConnection().Execute(sql, parameters);
        }
    }
}
