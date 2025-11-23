using System.ComponentModel.DataAnnotations;

namespace Scheduler.Application.Appointements.Dtos;

public class GetAvailableAppointementDto(Guid clientId, DateOnly appointementDate)
{
    [Required]
    public Guid ClientId { get; protected set; } = clientId;

    [DataType(DataType.Date)]
    [Required]
    public DateOnly AppointementDate { get; protected set; } = appointementDate;

}