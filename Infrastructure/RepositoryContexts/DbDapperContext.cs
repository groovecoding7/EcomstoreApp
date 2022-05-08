using System.Data;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.RepositoriesContexts
{
    public class DbDapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DbDapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateConnection()
        {
            var connectionString = new SqliteConnectionStringBuilder(_connectionString)
            {
                Mode = SqliteOpenMode.ReadWriteCreate
            }.ToString();
            return new SqliteConnection(connectionString);
        }

    }

}
