namespace Scheduler.Data.Patients.Repositories;

public class InMemoryPatientRepository : IPatientRepository
{
    private readonly List<Patient> _patients;

    public InMemoryPatientRepository()
    {
        _patients = [];
    }

    public Patient Create(Guid clientId, string name)
    {
        Patient patient = Patient.Create(clientId, name);
        _patients.Add(patient);
        return patient;
    }

    public IReadOnlyCollection<Patient> GetAll()
    {
        return _patients.AsReadOnly();
    }

    public Patient Get(Guid id)
    {
        Patient? patient = _patients.FirstOrDefault(c => c.Id == id);
        if (patient is null)
        {
            throw new KeyNotFoundException($"Not patient for Id {id}");
        }
        return patient;
    }

    public Patient Update(Guid id, Guid? clientId, string? name)
    {
        Patient? patient = _patients.FirstOrDefault(c => c.Id == id);
        if (patient is null)
        {
            throw new KeyNotFoundException($"Not patient for Id {id}");
        }
        patient.Update(clientId ?? patient.ClientId, name ?? patient.Name);
        return patient;
    }
}
