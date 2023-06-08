using System.Data;
using System.Data.SqlClient;

namespace Infra.Data.DbContext
{
    public class SqlServerDataBaseContext : ISqlServerDataBaseContext
    {
        private readonly IDbConnection _connection;

        public SqlServerDataBaseContext(string connectionString) 
        { 
            _connection = new SqlConnection(connectionString);

            if (Connection.State != ConnectionState.Open)
                Connection.Open();
        }

        public IDbConnection Connection 
        { 
            get 
            { 
                return _connection; 
            } 
        }

        public void Dispose() => Connection.Dispose();
    }
}
