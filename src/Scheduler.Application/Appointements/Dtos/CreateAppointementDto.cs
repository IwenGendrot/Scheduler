using System.ComponentModel.DataAnnotations;

namespace Scheduler.Application.Appointements.Dtos;

public class CreateAppointementDto
{
    public CreateAppointementDto(Guid clientId, Guid patientId, DateTime appointementTime)
    {
        ClientId = clientId;
        PatientId = patientId;
        AppointementTime = appointementTime;
    }

    [Required]
    public Guid ClientId { get; protected set; }

    [Required]
    public Guid PatientId { get; protected set; }

    [Required]
    public DateTime AppointementTime { get; protected set; }
}
