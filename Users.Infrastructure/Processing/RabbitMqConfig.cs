namespace Users.Infrastructure.Processing;

public record RabbitMqConfig
{
    public string Host { get; init; }
    public ushort Port { get; init; }
    public string VirtualHost { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
}