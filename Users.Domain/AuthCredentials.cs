using System;
using Domain.Shared.Business.Aggregates;

namespace Users.Domain;

public class AuthCredentials : Entity
{
    private string _name;
    private string _password;

    private DateTime? _passwordExpirationDate;

    private AuthCredentials() {}

    public AuthCredentials(string name) => _name = name;

    public static AuthCredentials Create(string name) => new AuthCredentials(name);

    public void SetPassword(string password)
    {
        _password = password;
        _passwordExpirationDate = DateTime.Now.AddMonths((int)Period.HalfAYear);
    }
}