using Scheduler.Application.Appointements.Dtos;
using Scheduler.Data.Appointements.Repositories;

namespace Scheduler.Application.Appointements;

public class AppointementService(IAppointementRepository appointementRepository) : IAppointementService
{
    private readonly IAppointementRepository _appointementRepository = appointementRepository;

    public Appointement Create(CreateAppointementDto dto)
    {
        return _appointementRepository.Create(dto.ClientId, dto.PatientId, dto.AppointementDate, dto.Hour);
    }

    public Appointement Get(Guid id)
    {
        return _appointementRepository.Get(id);
    }

    public IReadOnlyCollection<int> GetAvailableHoursOnDate(GetAvailableAppointementDto dto)
    {
        return _appointementRepository.GetAvailableHoursOnDate(dto.ClientId, dto.AppointementDate);
    }

    public IReadOnlyCollection<Appointement> GetAll()
    {
        return _appointementRepository.GetAll();
    }

    public IReadOnlyCollection<Appointement> GetForClient(Guid clientId)
    {
        return _appointementRepository.GetForClient(clientId);
    }

    public IReadOnlyCollection<Appointement> GetForClientAndPatient(Guid clientId, Guid patientId)
    {
        return _appointementRepository.GetForClientAndPatient(clientId, patientId);
    }

    public IReadOnlyCollection<Appointement> GetForPatient(Guid patientId)
    {
        return _appointementRepository.GetForPatient(patientId);
    }

    public Appointement Update(Guid id, UpdateAppointementDto dto)
    {
        return _appointementRepository.Update(id, dto.ClientId, dto.PatientId, dto.AppointementDate, dto.Hour);
    }
}
