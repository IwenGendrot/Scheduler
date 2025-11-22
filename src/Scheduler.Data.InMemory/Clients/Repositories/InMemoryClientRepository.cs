
namespace Scheduler.Data.Clients.Repositories;

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
        Client? client = _clients.FirstOrDefault(c => c.Id == id);
        if (client is null)
        {
            throw new KeyNotFoundException($"Not client for Id : {id}");
        }
        return client;
    }

    public Client Update(Guid id, string name)
    {
        Client? client = _clients.FirstOrDefault(c => c.Id == id);
        if (client is null)
        {
            throw new KeyNotFoundException($"Not client for Id {id}");
        }
        client.Update(name);
        return client;
    }
}
