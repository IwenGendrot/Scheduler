using Scheduler.Application.Appointements.Dtos;
using Scheduler.Data.Appointements.Repositories;
using Scheduler.Data.Clients.Repositories;
using Scheduler.Data.Patients.Repositories;

namespace Scheduler.Application.Appointements;

public class AppointementService(IAppointementRepository appointementRepository, IClientRepository clientRepository, IPatientRepository patientRepository) : IAppointementService
{
    private readonly IAppointementRepository _appointementRepository = appointementRepository;
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IPatientRepository _patientRepository = patientRepository;

    public Appointement Create(CreateAppointementDto dto)
    {
        _clientRepository.Get(dto.ClientId);
        _patientRepository.Get(dto.PatientId);
        return _appointementRepository.Create(dto.ClientId, dto.PatientId, dto.AppointementDate, dto.Hour);
    }

    public Appointement Get(Guid id)
    {
        return _appointementRepository.Get(id);
    }

    public IReadOnlyCollection<int> GetAvailableHoursOnDate(GetAvailableAppointementDto dto)
    {
        return _appointementRepository.GetAvailableHoursOnDate(dto.AppointementDate);
    }

    public IReadOnlyCollection<Appointement> GetAll()
    {
        return _appointementRepository.GetAll();
    }

    public IReadOnlyCollection<Appointement> GetForClient(Guid clientId)
    {
        _clientRepository.Get(clientId);
        return _appointementRepository.GetForClient(clientId);
    }

    public IReadOnlyCollection<Appointement> GetForClientAndPatient(Guid clientId, Guid patientId)
    {
        _clientRepository.Get(clientId);
        _patientRepository.Get(patientId);
        return _appointementRepository.GetForClientAndPatient(clientId, patientId);
    }

    public IReadOnlyCollection<Appointement> GetForPatient(Guid patientId)
    {
        _patientRepository.Get(patientId);
        return _appointementRepository.GetForPatient(patientId);
    }

    public Appointement Update(Guid id, UpdateAppointementDto dto)
    {
        return _appointementRepository.Update(id, dto.AppointementDate, dto.Hour);
    }
}
