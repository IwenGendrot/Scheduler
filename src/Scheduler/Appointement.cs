namespace Scheduler;

public class Appointement : Entity
{
    public Guid ClientId { get; private set; }

    public Guid PatientId { get; private set; }

    public DateOnly AppointementDate { get; private set; }

    public int Hour { get; private set; }

    public static readonly TimeSpan AppointementDuration = TimeSpan.FromMinutes(30);

    public static readonly DayOfWeek[] ClosedDays = [DayOfWeek.Saturday, DayOfWeek.Sunday];

    public const int OpeningHour = 9;

    public const int ClosingHour = 19;

    public Appointement(Guid id, Guid clientId, Guid patientId, DateOnly appointementDate, int hour)
    {
        Id = id;
        ClientId = clientId;
        PatientId = patientId;
        AppointementDate = appointementDate;
        Hour = hour;
    }

    public static Appointement Create(Guid clientId, Guid patientId, DateOnly appointementDate, int hour) => new(Guid.NewGuid(), clientId, patientId, appointementDate, hour);

    public void Update(DateOnly appointementTime, int hour)
    {
        AppointementDate = appointementTime;
        Hour = hour;
    }
}
