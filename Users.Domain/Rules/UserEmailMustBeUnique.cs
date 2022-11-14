using Domain.Shared.Business.Rules;

namespace Users.Domain.Rules;

public class UserEmailMustBeUnique : IBusinessRule
{
    private readonly IEmailAddressUniqueChecker _uniqueChecker;
    private readonly string _email;

    public UserEmailMustBeUnique(
        IEmailAddressUniqueChecker uniqueChecker,
        string email
    )
    {
        this._uniqueChecker = uniqueChecker;
        this._email = email;
    }

    public string Message => "User with this email address already exist";

    public bool IsBroken() => !this._uniqueChecker.IsUnique(_email);
}