namespace Scheduler;

public class Client
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public Client(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Client Create(string name) => new(Guid.NewGuid(), name);

    public void Update(string name) => Name = name;

    public const int MaxNameLength = 128;
}