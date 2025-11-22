namespace Scheduler.Application.Appointements.Dtos;

public class UpdateAppointementDto
{
    public Guid Id { get; private set; }

    public DateTime AppointementTime { get; private set; }

    public UpdateAppointementDto(Guid id, DateTime appointementTime)
    {
        Id = id;
        AppointementTime = appointementTime;
    }
}
