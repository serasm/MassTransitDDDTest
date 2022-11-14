using System;
using System.Text.Json.Serialization;
using Domain.Shared.Events.DomainEvents;

namespace Users.Domain.DomainEvents;

public class UserRegisteredDomainEvent : DomainEventBase<UserRegisteredSimplifiedEvent>
{
    public UserRegisteredDomainEvent(UserUniqueId uniqueId) : base(uniqueId)
    {
    }

    public override UserRegisteredSimplifiedEvent GenerateEvent()
    {
        return new UserRegisteredSimplifiedEvent((UserUniqueId)UniqueId, base.OccuredOn);
    }
}

public class UserRegisteredSimplifiedEvent
{
    public Guid UserUniqueId { get; init; }
    public DateTime OccuredOn { get; init; }
    
    public UserRegisteredSimplifiedEvent(UserUniqueId uniqueId, DateTime occuredOn)
    {
        UserUniqueId = uniqueId.AlternateId;
        OccuredOn = occuredOn;
    }
    
    [JsonConstructor]
    public UserRegisteredSimplifiedEvent(Guid userUniqueId, DateTime occuredOn)
    {
        UserUniqueId = userUniqueId;
        OccuredOn = occuredOn;
    }
}