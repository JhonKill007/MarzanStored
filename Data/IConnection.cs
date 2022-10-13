using Microsoft.Data.SqlClient;

namespace MarzanStored.Data
{
    public interface IConnection
    {
        SqlConnection GetConnection();
    }
}
