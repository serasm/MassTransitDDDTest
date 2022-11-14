using System;
using Domain.Shared.Business.Aggregates;
using Domain.Shared.Business.UniqueIdValueObjects;
using Users.Domain.DomainEvents;
using Users.Domain.Rules;

namespace Users.Domain;

public class User : Aggregate<UserUniqueId>
{
    private AuthCredentials _credentials;

    private string _emailAddress;

    private bool _welcomeEmailWasSend;

    private User()
    {
        
    }

    private User(
        string email,
        string name
    ) : base(new UserUniqueId(Guid.NewGuid()))
    {
        this._emailAddress = email;
        this._credentials = AuthCredentials.Create(name);
        this._welcomeEmailWasSend = false;

        this.AddDomainEvent(new UserRegisteredDomainEvent(this.UniqueId));
        this.AddDomainEvent(new UserPasswordSetDomainEvent(this.UniqueId));
    }

    public static User Register(
        string email,
        string name,
        IEmailAddressUniqueChecker uniqueChecker
    )
    {
        // Only two rules, the other rules will be used in other methods
        CheckRule(new UserEmailMustBeUnique(uniqueChecker, email));
        CheckRule(new UsernameMustHaveMoreThanOneCharacter(email));

        return new User(email, name);
    }

    public void ChangePassword(
        string newPassword
    )
    {
        CheckRule(new UserAccountPasswordMustHaveAtLeastOneCapitalLetter(newPassword));
        CheckRule(new UserAccountPasswordMustHaveAtLeastOneDigit(newPassword));
        CheckRule(new UserAccountPasswordMustHaveAtLeastOneSymbol(newPassword));

        this._credentials.SetPassword(newPassword);
    }

    public void MarkAsWelcomed()
    {
        this._welcomeEmailWasSend = true;
    }
}