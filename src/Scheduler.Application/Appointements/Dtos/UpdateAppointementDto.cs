using System.ComponentModel.DataAnnotations;

namespace Scheduler.Application.Appointements.Dtos;

public class UpdateAppointementDto(DateOnly appointementDate, int hour)
{
    [Required]
    public DateOnly AppointementDate = appointementDate;

    [Required, Range(minimum: 9, maximum: 18)]
    public int Hour { get; set; } = hour;
}
