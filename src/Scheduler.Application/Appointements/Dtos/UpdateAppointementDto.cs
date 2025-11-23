namespace Scheduler.Application.Appointements.Dtos;

public class UpdateAppointementDto(Guid clientId, Guid patientId, DateOnly appointementDate, int hour) : CreateAppointementDto(clientId, patientId, appointementDate, hour)
{
}
