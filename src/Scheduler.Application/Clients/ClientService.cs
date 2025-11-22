using Scheduler.Application.Clients.Dtos;
using Scheduler.Data.Clients.Repositories;

namespace Scheduler.Application.Clients;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public Client Create(CreateClientDto dto)
    {
        return _clientRepository.Create(dto.Name);
    }

    public IReadOnlyCollection<Client> GetAll()
    {
        return _clientRepository.GetAll();
    }

    public Client Get(Guid clientId)
    {
        return _clientRepository.Get(clientId);
    }

    public Client Update(Guid id, UpdateClientDto dto)
    {
        return _clientRepository.Update(id, dto.Name);
    }
}
