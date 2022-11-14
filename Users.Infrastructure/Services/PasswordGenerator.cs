using System.Security.Cryptography;
using Users.Application.Configuration.Services;

namespace Users.Infrastructure.Services;

public class PasswordGenerator : IPasswordGenerator
{
    private int PasswordLength = 10;

    private RNGCryptoServiceProvider Provider;

    private static string CapitalLetters = "QWERTYUIOPASDFGHJKLZXCVBNM";
    private static string SmallLetters = "qwertyuiopasdfghjklzxcvbnm";
    private static string Digits = "0123456789";
    private static string SpecialCharacters = "!@#$%^&*()-_=+<,>.";
    private string AllChar = CapitalLetters + SmallLetters + Digits + SpecialCharacters;

    public PasswordGenerator()
    {
        Provider = new RNGCryptoServiceProvider();
    }
    
    private char GenerateChar(string availableChars)
    {
        var byteArray = new byte[1];
        char c;
        do
        {
            Provider.GetBytes(byteArray);
            c = (char)byteArray[0];
        } while (!availableChars.Any(x => x == c));

        return c;
    }

    public void SetPasswordLength(int length) => this.PasswordLength = length;

    public string Generate()
    {
        return "1!asAS";
    }
}