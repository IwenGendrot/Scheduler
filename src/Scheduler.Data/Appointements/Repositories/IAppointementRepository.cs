namespace Scheduler.Data.Appointements.Repositories;

public interface IAppointementRepository
{
    Appointement Create(Guid clientId, Guid patientId, DateTime appointementTime);

    IReadOnlyCollection<Appointement> GetAll();

    Appointement Get(Guid id);

    IReadOnlyCollection<Appointement> GetForClient(Guid clientId);

    IReadOnlyCollection<Appointement> GetForClientAndPatient(Guid clientId, Guid patientId);

    IReadOnlyCollection<Appointement> GetForPatient(Guid patientId);

    Appointement Update(Guid id, DateTime appointementTime);
}
