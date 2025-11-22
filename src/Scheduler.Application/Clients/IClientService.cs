using Scheduler.Application.Clients.Dtos;

namespace Scheduler.Application.Clients;

public interface IClientService
{
    Client Create(CreateClientDto dto);

    IReadOnlyCollection<Client> GetAll();

    Client Get(Guid clientId);

    Client Update(Guid id, UpdateClientDto dto);
}
