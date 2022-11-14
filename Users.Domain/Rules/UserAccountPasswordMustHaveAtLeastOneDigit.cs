using System.Linq;
using Domain.Shared.Business.Rules;

namespace Users.Domain.Rules;

public class UserAccountPasswordMustHaveAtLeastOneDigit : IBusinessRule
{
    private readonly string _password;

    public UserAccountPasswordMustHaveAtLeastOneDigit(
        string password
    )
    {
        this._password = password;
    }

    public string Message => "User password must contain at least one digit";

    public bool IsBroken() => !(this._password.Any(char.IsDigit));
}