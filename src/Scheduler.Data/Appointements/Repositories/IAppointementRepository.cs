namespace Scheduler.Data.Appointements.Repositories;

public interface IAppointementRepository
{
    Appointement Create(Guid clientId, Guid patientId, DateOnly appointementDate, int hour);

    IReadOnlyCollection<Appointement> GetAll();

    IReadOnlyCollection<int> GetAvailableHoursOnDate(Guid clientId, DateOnly appointementDate);

    Appointement Get(Guid id);

    IReadOnlyCollection<Appointement> GetForClient(Guid clientId);

    IReadOnlyCollection<Appointement> GetForClientAndPatient(Guid clientId, Guid patientId);

    IReadOnlyCollection<Appointement> GetForPatient(Guid patientId);

    Appointement Update(Guid id, Guid clientId, Guid patientId, DateOnly appointementDate, int hour);
}
