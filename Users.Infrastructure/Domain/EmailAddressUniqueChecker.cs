using Dapper;
using Domain.Shared.Database;
using Users.Domain;

namespace Users.Infrastructure.Domain;

public class EmailAddressUniqueChecker : IEmailAddressUniqueChecker
{
    private readonly ISqlConnectionFactory _connectionFactory;

    private const string SQL_QUERY = "SELECT COUNT(*) FROM users_app.\"Users\" WHERE \"EmailAddress\" = @address";
    
    public EmailAddressUniqueChecker(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    public bool IsUnique(string email)
    {
        var parameters = new { address = email };
        
        var connection = _connectionFactory.GetOpenConnection();

        var result = connection.ExecuteScalar<int>(SQL_QUERY, parameters);

        return result == 0;
    }
}