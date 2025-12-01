using Scheduler.Data.Clients.Repositories;

namespace Scheduler.Data.InMemory.Clients.Repositories;

public class InMemoryClientRepository : IClientRepository
{
    private readonly List<Client> _clients;

    public InMemoryClientRepository()
    {
        _clients = [];
    }

    public Client Create(string name)
    {
        Client client = Client.Create(name);
        _clients.Add(client);
        return client;
    }

    public IReadOnlyCollection<Client> GetAll()
    {
        return _clients.AsReadOnly();
    }

    public Client Get(Guid id)
    {
        return _clients.FirstOrDefault(c => c.Id == id) ?? throw new KeyNotFoundException($"Not client for Id : {id}");
    }

    public Client Update(Guid id, string name)
    {
        Client client = _clients.FirstOrDefault(c => c.Id == id) ?? throw new KeyNotFoundException($"Not client for Id {id}");
        client.Update(name);
        return client;
    }
}
