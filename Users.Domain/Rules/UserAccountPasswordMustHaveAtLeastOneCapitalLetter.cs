using System.Linq;
using Domain.Shared.Business.Rules;

namespace Users.Domain.Rules;

public class UserAccountPasswordMustHaveAtLeastOneCapitalLetter : IBusinessRule
{
    private readonly string _password;

    public UserAccountPasswordMustHaveAtLeastOneCapitalLetter(
        string password
    )
    {
        this._password = password;
    }

    public string Message => "Password must contain at least one capital letter";

    public bool IsBroken() => !(this._password.Any(char.IsUpper));
}