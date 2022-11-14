using Domain.Shared.Business.Rules;

namespace Domain.Shared.Business.Aggregates;

public abstract class Entity
{
    private int _id;
    public int Id => _id;
    
    protected static void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
}