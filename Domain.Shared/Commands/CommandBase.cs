namespace Domain.Shared.Commands;

public class CommandBase : ICommand
{
    public CommandBase(Guid id)
    {
        this.Id = id;
    }

    public CommandBase()
    {
        this.Id = Guid.NewGuid();
    }
    
    public Guid Id { get; }
}