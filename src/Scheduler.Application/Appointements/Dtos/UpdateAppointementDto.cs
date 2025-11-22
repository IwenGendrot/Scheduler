using System.ComponentModel.DataAnnotations;

namespace Scheduler.Application.Appointements.Dtos;

public class UpdateAppointementDto(Guid id, Guid clientId, Guid patientId, DateOnly appointementDate, int hour) : CreateAppointementDto(clientId, patientId, appointementDate, hour)
{
    [Required]
    public Guid Id { get; private set; } = id;
}
