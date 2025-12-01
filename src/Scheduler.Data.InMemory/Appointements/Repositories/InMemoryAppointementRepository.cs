using Scheduler.Data.Appointements.Repositories;

namespace Scheduler.Data.InMemory.Appointements.Repositories;

public class InMemoryAppointementRepository : IAppointementRepository
{
    private readonly List<Appointement> _appointements;
    private static readonly int[] BookableHours = [9, 10, 11, 12, 13, 14, 15, 16, 17, 18];

    public InMemoryAppointementRepository()
    {
        _appointements = [];
    }

    public Appointement Create(Guid clientId, Guid patientId, DateOnly appointementDate, int hour)
    {
        if (_appointements.FirstOrDefault(a => (a.ClientId == clientId || a.PatientId == patientId) && a.AppointementDate == appointementDate && a.Hour == hour) is not null)
        {
            throw new ArgumentException("Client or patient is already booked at this time");
        }
        Appointement appointement = Appointement.Create(clientId, patientId, appointementDate, hour);
        _appointements.Add(appointement);
        return appointement;
    }

    public IReadOnlyCollection<Appointement> GetAll()
    {
        return _appointements.AsReadOnly();
    }

    public IReadOnlyCollection<int> GetAvailableHoursOnDate(DateOnly appointementDate)
    {
        IEnumerable<int> bookedHours = _appointements.Where(a => a.AppointementDate == appointementDate).Select(a => a.Hour);
        return BookableHours.Except(bookedHours).ToList().AsReadOnly();
    }

    public Appointement Get(Guid id)
    {
        Appointement appointement = _appointements.FirstOrDefault(a => a.Id == id) ?? throw new KeyNotFoundException($"No appointement for id {id}");
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

    public Appointement Update(Guid id, DateOnly appointementDate, int hour)
    {
        Appointement appointement = Get(id);
        if (_appointements.FirstOrDefault(a => a.AppointementDate == appointementDate && a.Hour == hour) is not null)
        {
            throw new ArgumentException("Client or patient is already booked at this time");
        }
        appointement.Update(appointementDate, hour);
        return appointement;
    }
}
