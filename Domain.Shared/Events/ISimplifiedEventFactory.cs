namespace Domain.Shared.Events;

public interface ISimplifiedEventFactory<TSimplifiedEvent>
{
    TSimplifiedEvent GenerateEvent();
}