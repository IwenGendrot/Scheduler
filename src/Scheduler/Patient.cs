namespace Scheduler;

public class Patient : Entity
{
    public Guid ClientId { get; private set; }

    public string Name { get; private set; }

    public Patient(Guid id, Guid clientId, string name)
    {
        Id = id;
        ClientId = clientId;
        Name = name;
    }

    public static Patient Create(Guid clientId, string name) => new(Guid.NewGuid(), clientId, name);

    public void Update(Guid clientId, string name)
    {
        ClientId = clientId;
        Name = name;
    }
}
