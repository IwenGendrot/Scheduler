namespace Scheduler.Data.Appointements.Repositories;

public class InMemoryAppointementRepository : IAppointementRepository
{
    private readonly List<Appointement> _appointements;

    public InMemoryAppointementRepository()
    {
        _appointements = [];
    }

    public Appointement Create(Guid clientId, Guid patientId, DateTime appointementTime)
    {
        Appointement appointement = Appointement.Create(clientId, patientId, appointementTime);
        _appointements.Add(appointement);
        return appointement;
    }

    public IReadOnlyCollection<Appointement> GetAll()
    {
        return _appointements.AsReadOnly();
    }

    public Appointement Get(Guid id)
    {
        Appointement? appointement = _appointements.FirstOrDefault(a => a.Id == id);
        if (appointement is null)
        {
            throw new KeyNotFoundException($"No appointement for id {id}");
        }
        return appointement;
    }

    public IReadOnlyCollection<Appointement> GetForClient(Guid clientId)
    {
        return _appointements.Where(c => c.ClientId == clientId).ToList().AsReadOnly();
    }

    public IReadOnlyCollection<Appointement> GetForClientAndPatient(Guid clientId, Guid patientId)
    {
        return _appointements.Where(c => c.ClientId == clientId && c.PatientId == patientId).ToList().AsReadOnly();
    }

    public IReadOnlyCollection<Appointement> GetForPatient(Guid patientId)
    {
        return _appointements.Where(c => c.PatientId == patientId).ToList().AsReadOnly();
    }

    public Appointement Update(Guid id, DateTime appointementTime)
    {
        Appointement? appointement = _appointements.FirstOrDefault(a => a.Id == id);
        if (appointement is null)
        {
            throw new KeyNotFoundException($"No appointement for id {id}");
        }
        appointement.Update(appointementTime);
        return appointement;
    }
}
