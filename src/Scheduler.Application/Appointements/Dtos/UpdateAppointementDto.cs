using System.ComponentModel.DataAnnotations;

namespace Scheduler.Application.Appointements.Dtos;

public class UpdateAppointementDto(Guid id, DateOnly appointementDate, int hour)
{
    [Required]
    public Guid Id { get; set; } = id;

    [Required]
    public DateOnly AppointementDate = appointementDate;

    [Required, Range(minimum: 9, maximum: 18)]
    public int Hour { get; set; } = hour;
}
