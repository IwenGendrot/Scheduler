using AwesomeAssertions;
using Moq;
using Scheduler.Application.Clients;
using Scheduler.Data.Clients.Repositories;

namespace Scheduler.Application.Tests.Clients;

public class ClientServiceTests
{
    private readonly ClientService _clientService;
    private readonly Mock<IClientRepository> _mockClientRepository;

    private readonly Guid _clientId;
    private readonly Guid _fakeClientId;
    private readonly string _clientName = "ClientName";
    private readonly Client _client;

    public ClientServiceTests()
    {
        _clientId = Guid.NewGuid();
        _fakeClientId = Guid.NewGuid();

        _mockClientRepository = new();
        _client = new(_clientId, _clientName);

        _mockClientRepository.Setup(r => r.Get(It.IsAny<Guid>())).Throws(new KeyNotFoundException());
        _mockClientRepository.Setup(r => r.Get(_clientId)).Returns(_client);

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
}
