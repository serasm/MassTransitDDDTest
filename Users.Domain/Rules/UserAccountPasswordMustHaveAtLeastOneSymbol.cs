using System.Linq;
using Domain.Shared.Business.Rules;

namespace Users.Domain.Rules;

public class UserAccountPasswordMustHaveAtLeastOneSymbol : IBusinessRule
{
    private readonly string _password;

    public UserAccountPasswordMustHaveAtLeastOneSymbol(
        string password
    )
    {
        this._password = password;
    }

    public string Message => "User password must contains at least one symbol";

    public bool IsBroken() => !(this._password.Any(ch => !char.IsLetterOrDigit(ch)));
}