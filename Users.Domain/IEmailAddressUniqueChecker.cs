namespace Users.Domain;

public interface IEmailAddressUniqueChecker
{
    bool IsUnique(string email);
}