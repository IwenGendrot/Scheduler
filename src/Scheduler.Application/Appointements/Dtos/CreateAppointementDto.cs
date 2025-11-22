using System.ComponentModel.DataAnnotations;

namespace Scheduler.Application.Appointements.Dtos;

public class CreateAppointementDto
{
    public CreateAppointementDto(Guid clientId, Guid patientId, DateOnly appointementDate, int hour)
    {
        ClientId = clientId;
        PatientId = patientId;
        AppointementDate = appointementDate;
        Hour = hour;
    }

    [Required]
    public Guid ClientId { get; protected set; }

    [Required]
    public Guid PatientId { get; protected set; }

    [DataType(DataType.Date)]
    [Required]
    public DateOnly AppointementDate { get; protected set; }

    [Required, Range(minimum: 9, maximum: 18)]
    public int Hour { get; protected set; }
}
