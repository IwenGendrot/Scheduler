namespace Scheduler;

public class Appointement(Guid ClientId, Guid PatientId, DateTime DateTime)
{
    public static Appointement Create(Guid clientId, Guid patientId, DateTime dateTime) => new(clientId, patientId, dateTime);

    public void Update(DateTime dateTime) => DateTime = dateTime;
}
