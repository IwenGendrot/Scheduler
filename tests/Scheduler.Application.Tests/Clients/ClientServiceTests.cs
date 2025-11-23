using AwesomeAssertions;
using Moq;
using Scheduler.Application.Clients;
using Scheduler.Application.Clients.Dtos;
using Scheduler.Data.Clients.Repositories;

namespace Scheduler.Application.Tests.Clients;

public class ClientServiceTests
{
    private readonly ClientService _clientService;
    private readonly Mock<IClientRepository> _mockClientRepository;

    private readonly Guid _clientId;
    private readonly Guid _fakeClientId;
    private readonly string _clientName = "ClientName";
    private readonly string _clientNewName = "NewName";
    private readonly Client _client;
    private readonly Client _newClient;

    public ClientServiceTests()
    {
        _clientId = Guid.NewGuid();
        _fakeClientId = Guid.NewGuid();

        _mockClientRepository = new();
        _client = new(_clientId, _clientName);
        _newClient = new(_clientId, _clientNewName);

        _mockClientRepository.Setup(r => r.Get(It.IsAny<Guid>())).Throws(new KeyNotFoundException());
        _mockClientRepository.Setup(r => r.Get(_clientId)).Returns(_client);

        _mockClientRepository.Setup(r => r.GetAll()).Returns([_client]);

        _mockClientRepository.Setup(r => r.Create(_clientName)).Returns(_client);

        _mockClientRepository.Setup(r => r.Update(_clientId, _clientNewName)).Returns(_newClient);
        _mockClientRepository.Setup(r => r.Update(_fakeClientId, It.IsAny<string>())).Throws(new KeyNotFoundException());

        _clientService = new(_mockClientRepository.Object);
    }


    [Fact]
    public void Get_WhenIdFound_ShouldReturnClient()
    {
        Client result = _clientService.Get(_clientId);
        result.Should().BeEquivalentTo(_client);
    }

    [Fact]
    public void Get_WhenIdNotFound_ShouldThrow()
    {
        Action action = () => _clientService.Get(_fakeClientId);
        action.Should().Throw<KeyNotFoundException>();
    }

    [Fact]
    public void GetAll_ShouldReturnClients()
    {
        IReadOnlyCollection<Client> result = _clientService.GetAll();
        Client client = result.Should().ContainSingle().Subject;
        client.Should().BeEquivalentTo(_client);
    }

    [Fact]
    public void Create_ShouldReturnCreatedClient()
    {
        Client result = _clientService.Create(new CreateClientDto(_clientName));
        result.Should().BeEquivalentTo(_client);
    }

    [Fact]
    void Update_ShouldReturnUpdatedClient()
    {
        Client result = _clientService.Update(_clientId, new UpdateClientDto(_clientNewName));
        result.Should().BeEquivalentTo(_newClient);
    }

    [Fact]
    void Update_WhenIdNotFound_ShouldThrows()
    {
        Action action = () => _clientService.Update(_fakeClientId, new UpdateClientDto(_clientNewName));
        action.Should().Throw<KeyNotFoundException>();
    }
}
