using System.ComponentModel.DataAnnotations;

namespace Scheduler.Application.Patients.Dtos;

public class CreatePatientDto
{
    public CreatePatientDto(Guid clientId, string name)
    {
        ClientId = clientId;
        Name = name;
    }

    [Required]
    public Guid ClientId { get; protected set; }

    [Required, StringLength(maximumLength: Client.MaxNameLength)]
    public string Name { get; protected set; }
}
