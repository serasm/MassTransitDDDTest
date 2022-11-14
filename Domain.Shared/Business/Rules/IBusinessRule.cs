namespace Domain.Shared.Business.Rules;

public interface IBusinessRule
{
    public string Message { get; }
    public bool IsBroken();
}