namespace Scheduler.Data.Patients.Repositories;

public interface IPatientRepository
{
    Patient Create(Guid clientId, string name);

    IReadOnlyCollection<Patient> GetAll();

    Patient Get(Guid id);

    Patient Update(Guid id, Guid? clientId, string? name);
}
