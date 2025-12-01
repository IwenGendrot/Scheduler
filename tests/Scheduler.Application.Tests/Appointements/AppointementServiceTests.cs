using AwesomeAssertions;
using Moq;
using Scheduler.Application.Appointements;
using Scheduler.Application.Appointements.Dtos;
using Scheduler.Data.Appointements.Repositories;
using Scheduler.Data.Clients.Repositories;
using Scheduler.Data.Patients.Repositories;

namespace Scheduler.Application.Tests.Appointements;

public class AppointementServiceTests
{
    private readonly AppointementService _appointementService;
    private readonly Mock<IAppointementRepository> _mockAppointementRepository;
    private readonly Mock<IClientRepository> _mockClientRepository;
    private readonly Mock<IPatientRepository> _mockPatientRepository;

    private readonly Appointement _appointement;
    private readonly Appointement _newAppointement;
    private readonly Guid _appointementId;
    private readonly Guid _fakeAppointementId;
    private readonly Guid _clientId;
    private readonly Guid _patientId;
    private readonly DateOnly _appointementDate;
    private readonly int _appointementHour;
    private readonly int _newAppointementHour;
    private readonly int[] _availableHours;

    public AppointementServiceTests()
    {
        _appointementId = Guid.NewGuid();
        _fakeAppointementId = Guid.NewGuid();
        _clientId = Guid.NewGuid();
        _patientId = Guid.NewGuid();
        _appointementDate = new DateOnly(2025, 11, 17);
        _appointementHour = 11;
        _newAppointementHour = 17;
        _availableHours = [10, 12, 13, 14, 15, 16, 17, 18];

        _mockAppointementRepository = new();
        _mockClientRepository = new();
        _mockPatientRepository = new();

        Client client = new(_clientId, "");
        _mockClientRepository.Setup(r => r.Get(It.IsAny<Guid>())).Throws(new KeyNotFoundException());
        _mockClientRepository.Setup(r => r.Get(_clientId)).Returns(client);

        Patient patient = new(_patientId, _clientId, "");
        _mockPatientRepository.Setup(r => r.Get(It.IsAny<Guid>())).Throws(new KeyNotFoundException());
        _mockPatientRepository.Setup(r => r.Get(_patientId)).Returns(patient);

        _appointement = new(_appointementId, _clientId, _patientId, _appointementDate, _appointementHour);
        _newAppointement = new(_appointementId, _clientId, _patientId, _appointementDate, _newAppointementHour);

        _mockAppointementRepository.Setup(r => r.Get(It.IsAny<Guid>())).Throws(new KeyNotFoundException());
        _mockAppointementRepository.Setup(r => r.Get(_appointementId)).Returns(_appointement);

        _mockAppointementRepository.Setup(r => r.GetAll()).Returns([_appointement]);

        _mockAppointementRepository.Setup(r => r.GetForClient(It.IsAny<Guid>())).Returns([]);
        _mockAppointementRepository.Setup(r => r.GetForClient(_clientId)).Returns([_appointement]);

        _mockAppointementRepository.Setup(r => r.GetForPatient(It.IsAny<Guid>())).Returns([]);
        _mockAppointementRepository.Setup(r => r.GetForPatient(_patientId)).Returns([_appointement]);

        _mockAppointementRepository.Setup(r => r.GetForClientAndPatient(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns([]);
        _mockAppointementRepository.Setup(r => r.GetForClientAndPatient(_clientId, _patientId)).Returns([_appointement]);

        _mockAppointementRepository.Setup(r => r.Create(_clientId, _patientId, _appointementDate, _appointementHour)).Returns(_appointement);

        _mockAppointementRepository.Setup(r => r.Update(_fakeAppointementId, It.IsAny<DateOnly>(), It.IsAny<int>())).Throws(new KeyNotFoundException());
        _mockAppointementRepository.Setup(r => r.Update(_appointementId, _appointementDate, _newAppointementHour)).Returns(_newAppointement);

        _mockAppointementRepository.Setup(r => r.GetAvailableHoursOnDate(_appointementDate)).Returns(_availableHours);

        _appointementService = new(_mockAppointementRepository.Object, _mockClientRepository.Object, _mockPatientRepository.Object);
    }

    [Fact]
    public void Get_ShouldReturnAppointement()
    {
        Appointement result = _appointementService.Get(_appointementId);
        result.Should().BeEquivalentTo(_appointement);
    }

    [Fact]
    public void Get_WhenAppointementNotFound_ShouldThrows()
    {
        Action action = () => _appointementService.Get(_fakeAppointementId);
        action.Should().Throw<KeyNotFoundException>();
    }

    [Fact]
    public void GetAllForClient_ShouldReturnAppointement()
    {
        IReadOnlyCollection<Appointement> result = _appointementService.GetForClient(_clientId);
        Appointement appointement = result.Should().ContainSingle().Subject;
        appointement.Should().BeEquivalentTo(_appointement);
    }

    [Fact]
    public void GetAllForClient_WhenClientNotFound_ShouldReturnEmpty()
    {
        Action action = () => _appointementService.GetForClient(_fakeAppointementId);
        action.Should().Throw<KeyNotFoundException>();
    }

    [Fact]
    public void GetAllForPatient_ShouldReturnAppointement()
    {
        IReadOnlyCollection<Appointement> result = _appointementService.GetForPatient(_patientId);
        Appointement appointement = result.Should().ContainSingle().Subject;
        appointement.Should().BeEquivalentTo(_appointement);
    }

    [Fact]
    public void GetAllForPatient_WhenPatientNotFound_ShouldReturnEmpty()
    {
        Action action = () => _appointementService.GetForPatient(_fakeAppointementId);
        action.Should().Throw<KeyNotFoundException>();
    }

    [Fact]
    public void GetForClientAndPatient_ShouldReturnAppointement()
    {
        IReadOnlyCollection<Appointement> result = _appointementService.GetForClientAndPatient(_clientId, _patientId);
        Appointement appointement = result.Should().ContainSingle().Subject;
        appointement.Should().BeEquivalentTo(_appointement);
    }

    [Fact]
    public void GetForClientAndPatient_WhenPatientNotFound_ShouldReturnEmpty()
    {
        Action action = () => _appointementService.GetForClientAndPatient(_fakeAppointementId, _fakeAppointementId);
        action.Should().Throw<KeyNotFoundException>();
    }

    [Fact]
    public void GetAll_ShouldReturnAppointement()
    {
        IReadOnlyCollection<Appointement> result = _appointementService.GetAll();
        Appointement appointement = result.Should().ContainSingle().Subject;
        appointement.Should().BeEquivalentTo(_appointement);
    }

    [Fact]
    public void Create_ShouldReturnCreatedAppointement()
    {
        Appointement result = _appointementService.Create(new CreateAppointementDto(_clientId, _patientId, _appointementDate, _appointementHour));
        result.Should().BeEquivalentTo(_appointement);
    }

    [Fact]
    public void Update_ShouldReturnUpdatedAppointement()
    {
        Appointement result = _appointementService.Update(_appointementId, new UpdateAppointementDto(_appointementDate, _newAppointementHour));
        result.Should().BeEquivalentTo(_newAppointement);
    }

    [Fact]
    public void Update_WhenAppointementNotFound_ShouldThrow()
    {
        Action action = () => _appointementService.Update(_fakeAppointementId, new UpdateAppointementDto(_appointementDate, _newAppointementHour));
        action.Should().Throw<KeyNotFoundException>();
    }

    [Fact]
    public void GetAvailableHoursOnDate_ShouldReturnAvailableTimes()
    {
        IReadOnlyCollection<int> result = _appointementService.GetAvailableHoursOnDate(new GetAvailableAppointementDto(_clientId, _appointementDate));
        result.Should().BeEquivalentTo(_availableHours);
    }
}
