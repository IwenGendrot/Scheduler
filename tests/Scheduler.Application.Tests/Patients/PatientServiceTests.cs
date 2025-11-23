using AwesomeAssertions;
using Moq;
using Scheduler.Application.Patients;
using Scheduler.Application.Patients.Dtos;
using Scheduler.Data.Patients.Repositories;

namespace Scheduler.Application.Tests.Patients;

public class PatientServiceTests
{
    private readonly PatientService _patientService;
    private readonly Mock<IPatientRepository> _mockPatientRepository;

    private readonly Guid _patientId;
    private readonly Guid _fakePatientId;
    private readonly Guid _clientId;
    private readonly Guid _newClientId;
    private readonly string _patientName = "PatientName";
    private readonly string _patientNewName = "NewName";
    private readonly Patient _patient;
    private readonly Patient _newPatient;

    public PatientServiceTests()
    {
        _patientId = Guid.NewGuid();
        _clientId = Guid.NewGuid();
        _newClientId = Guid.NewGuid();
        _fakePatientId = Guid.NewGuid();

        _mockPatientRepository = new();
        _patient = new(_patientId, _clientId, _patientName);
        _newPatient = new(_patientId, _newClientId, _patientNewName);

        _mockPatientRepository.Setup(r => r.Get(It.IsAny<Guid>())).Throws(new KeyNotFoundException());
        _mockPatientRepository.Setup(r => r.Get(_patientId)).Returns(_patient);

        _mockPatientRepository.Setup(r => r.GetAll()).Returns([_patient]);

        _mockPatientRepository.Setup(r => r.Create(_clientId, _patientName)).Returns(_patient);

        _mockPatientRepository.Setup(r => r.Update(_patientId, _newClientId, _patientNewName)).Returns(_newPatient);
        _mockPatientRepository.Setup(r => r.Update(_fakePatientId, It.IsAny<Guid>(), It.IsAny<string>())).Throws(new KeyNotFoundException());

        _patientService = new(_mockPatientRepository.Object);
    }


    [Fact]
    public void Get_WhenIdFound_ShouldReturnPatient()
    {
        Patient result = _patientService.Get(_patientId);
        result.Should().BeEquivalentTo(_patient);
    }

    [Fact]
    public void Get_WhenIdNotFound_ShouldThrow()
    {
        Action action = () => _patientService.Get(_fakePatientId);
        action.Should().Throw<KeyNotFoundException>();
    }

    [Fact]
    public void GetAll_ShouldReturnPatients()
    {
        IReadOnlyCollection<Patient> result = _patientService.GetAll();
        result.Should().NotBeNullOrEmpty();
        result.Should().Contain(_patient);
    }

    [Fact]
    public void Create_ShouldReturnCreatedPatient()
    {
        Patient result = _patientService.Create(new CreatePatientDto(_clientId, _patientName));
        result.Should().BeEquivalentTo(_patient);
    }

    [Fact]
    void Update_ShouldReturnUpdatedPatient()
    {
        Patient result = _patientService.Update(_patientId, new UpdatePatientDto(_newClientId, _patientNewName));
        result.Should().BeEquivalentTo(_newPatient);
    }

    [Fact]
    void Update_WhenIdNotFound_ShouldThrows()
    {
        Action action = () => _patientService.Update(_fakePatientId, new UpdatePatientDto(_clientId, _patientNewName));
        action.Should().Throw<KeyNotFoundException>();
    }
}
