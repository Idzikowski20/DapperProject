using Microsoft.Data.SqlClient;

namespace DapperProject.Factory
{
    public interface IDbConnectionFactory
    {
        SqlConnection CreateConnection();
    }
}
