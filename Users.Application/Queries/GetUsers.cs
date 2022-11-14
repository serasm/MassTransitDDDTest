using Dapper;
using Domain.Shared.Database;
using Domain.Shared.Queries;
using MassTransit;

namespace Users.Application.Queries;

public class UserDto
{
    public string EmailAddress { get; init; }
    public bool WelcomeEmail { get; init; }
}

public class UsersDto
{
    public UserDto[] Users { get; set; }
}

public class GetUsersQuery : IQuery<UsersDto>
{
    public GetUsersQuery() {}
}

public class GetUsersQueryHandler : QueryHandlerBase<GetUsersQuery, UsersDto>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    private const string SQL_QUERY = "SELECT \"EmailAddress\", \"WelcomeEmailWasSend\" as WelcomeEmail FROM users_app.\"Users\"";
    
    public GetUsersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    
    public override async Task<UsersDto> HandleQueryAsync(ConsumeContext<GetUsersQuery> context)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        var users = await connection.QueryAsync<UserDto>(SQL_QUERY);

        return new UsersDto
        {
            Users = users.ToArray()
        };
    }
}