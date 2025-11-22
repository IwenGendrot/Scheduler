using Scheduler.Application.Appointements.Dtos;

namespace Scheduler.Application.Appointements;

public interface IAppointementService
{
    Appointement Create(CreateAppointementDto dto);

    IReadOnlyCollection<int> GetAvailableHoursOnDate(Guid clientId, DateOnly appointementDate);

    IReadOnlyCollection<Appointement> GetAll();

    Appointement Get(Guid id);

    IReadOnlyCollection<Appointement> GetForClient(Guid clientId);

    IReadOnlyCollection<Appointement> GetForClientAndPatient(Guid clientId, Guid patientId);

    IReadOnlyCollection<Appointement> GetForPatient(Guid patientId);

    Appointement Update(Guid id, UpdateAppointementDto dto);
}
