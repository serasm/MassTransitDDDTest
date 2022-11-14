using System.Data;

namespace Domain.Shared.Database;

public interface ISqlConnectionFactory
{
    IDbConnection GetOpenConnection();
}