using System.ComponentModel.DataAnnotations;

namespace Scheduler.Application.Appointements.Dtos;

public class UpdateAppointementDto : CreateAppointementDto
{
    public UpdateAppointementDto(Guid id, Guid clientId, Guid patientId, DateOnly appointementDate, int hour) : base(clientId, patientId, appointementDate, hour)
    {
        Id = id;
    }

    [Required]
    public Guid Id { get; private set; }
}
