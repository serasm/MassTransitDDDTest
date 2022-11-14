namespace Domain.Shared.Business.UniqueIdValueObjects;

public interface IUniqueId
{
    Guid AlternateId { get; }
}