using Scheduler.Application.Patients.Dtos;

namespace Scheduler.Application.Patients;

public interface IPatientService
{
    Patient Create(CreatePatientDto dto);

    IReadOnlyCollection<Patient> GetAll();

    Patient Get(Guid clientId);

    Patient Update(Guid id, UpdatePatientDto dto);
}
