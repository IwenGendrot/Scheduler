namespace Scheduler.Data.Clients.Repositories;

public interface IClientRepository
{
    Client Get(Guid id);

    IReadOnlyCollection<Client> GetAll();

    Client Create(string name);

    Client Update(Guid id, string name);
}
