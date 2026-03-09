using Microsoft.Data.SqlClient;

namespace DapperProject.Factory
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;
        public SqlConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
