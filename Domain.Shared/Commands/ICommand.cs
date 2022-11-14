namespace Domain.Shared.Commands;

public interface ICommand
{
    Guid Id { get; }
}