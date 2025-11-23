namespace Scheduler;

public class Client : Entity
{
    public string Name { get; private set; }

    public const int MaxNameLength = 128;

    public Client(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Client Create(string name) => new(Guid.NewGuid(), name);

    public void Update(string name) => Name = name;
}