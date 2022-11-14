using System;
using System.Text.Json.Serialization;
using Domain.Shared.Events.DomainEvents;

namespace Users.Domain.DomainEvents;

public class UserPasswordSetDomainEvent : DomainEventBase<UserPasswordSetSimplifiedEvent>
{
    public UserPasswordSetDomainEvent(UserUniqueId uniqueId) : base(uniqueId)
    {
    }

    public override UserPasswordSetSimplifiedEvent GenerateEvent()
    {
        return new UserPasswordSetSimplifiedEvent((UserUniqueId)UniqueId, OccuredOn);
    }
}

public class UserPasswordSetSimplifiedEvent
{
    public Guid UserUniqueId { get; init; }
    public DateTime OccuredOn { get; init; }
    
    public UserPasswordSetSimplifiedEvent(UserUniqueId uniqueId, DateTime occuredOn)
    {
        UserUniqueId = uniqueId.AlternateId;
        OccuredOn = occuredOn;
    }

    [JsonConstructor]
    public UserPasswordSetSimplifiedEvent(Guid userUniqueId, DateTime occuredOn)
    {
        UserUniqueId = userUniqueId;
        OccuredOn = occuredOn;
    }
}