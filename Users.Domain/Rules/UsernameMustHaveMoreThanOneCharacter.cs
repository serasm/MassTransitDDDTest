using Domain.Shared.Business.Rules;

namespace Users.Domain.Rules;

public class UsernameMustHaveMoreThanOneCharacter : IBusinessRule
{
    private readonly string _userName;

    public UsernameMustHaveMoreThanOneCharacter(
        string username
    )
    {
        this._userName = username;
    }

    public string Message => "Username must have more than one character";

    public bool IsBroken()
    {
        var numbersOfCharacters = _userName.Length;

        return !(numbersOfCharacters > 1);
    }
}