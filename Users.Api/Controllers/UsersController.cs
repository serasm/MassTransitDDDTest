using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Users.Api.Models;
using Users.Application.Commands;
using Users.Application.Queries;

namespace Users.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<UserDto>> Get()
    {
        var client = _mediator.CreateRequestClient<GetUsersQuery>();
        
        var result = await client.GetResponse<UsersDto>(new GetUsersQuery());

        return result.Message.Users;
    }
    
    [HttpPost]
    public async Task<RegisteredUserDataDto> Post([FromBody]UserRegistrationForm value)
    {
        var client = _mediator.CreateRequestClient<RegisterNewUserCommand>();
        
        var result = await client.GetResponse<RegisteredUserDataDto>(new RegisterNewUserCommand(value.Email, value.Username));

        return result.Message;
    }
}