using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.RepositoryContexts
{
    
    public class MemoryContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public MemoryContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void CreateConnection()
        {
           
        }
    }
}
