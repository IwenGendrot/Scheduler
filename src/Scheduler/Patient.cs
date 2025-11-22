namespace Scheduler;

public class Patient(Guid Id, Guid ClientId, string Name)
{
    public static Patient Create(Guid clientId, string name) => new(Guid.NewGuid(), clientId, name);

    public void Update(string name) => Name = name;
}
