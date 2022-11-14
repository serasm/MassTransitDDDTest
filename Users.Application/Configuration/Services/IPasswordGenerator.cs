namespace Users.Application.Configuration.Services;

public interface IPasswordGenerator
{
    void SetPasswordLength(int length);
    string Generate();
}