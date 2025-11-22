using System.ComponentModel.DataAnnotations;

namespace Scheduler.Application.Appointements.Dtos;

public class CreateAppointementDto(Guid clientId, Guid patientId, DateOnly appointementDate, int hour)
{
    [Required]
    public Guid ClientId { get; protected set; } = clientId;

    [Required]
    public Guid PatientId { get; protected set; } = patientId;

    [DataType(DataType.Date)]
    [Required]
    public DateOnly AppointementDate { get; protected set; } = appointementDate;

    [Required, Range(minimum: 9, maximum: 18)]
    public int Hour { get; protected set; } = hour;
}
