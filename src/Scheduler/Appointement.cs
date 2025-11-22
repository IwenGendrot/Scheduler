namespace Scheduler;

public class Appointement : Entity
{
    public Guid ClientId { get; private set; }

    public Guid PatientId { get; private set; }

    public DateTime AppointementTime { get; private set; }

    public static readonly TimeSpan AppointementDuration = TimeSpan.FromMinutes(30);

    public static readonly DayOfWeek[] ClosedDays = [DayOfWeek.Saturday, DayOfWeek.Sunday];

    public const int OpeningHour = 9;

    public const int ClosingHour = 19;

    public Appointement(Guid id, Guid clientId, Guid patientId, DateTime appointementTime)
    {
        Id = id;
        ClientId = clientId;
        PatientId = patientId;
        AppointementTime = appointementTime;
    }

    public static Appointement Create(Guid clientId, Guid patientId, DateTime appointementTime) => new(Guid.NewGuid(), clientId, patientId, appointementTime);

    public void Update(DateTime appointementTime) => AppointementTime = appointementTime;
}
