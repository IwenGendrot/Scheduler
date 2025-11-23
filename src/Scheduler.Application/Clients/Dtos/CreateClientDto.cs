using System.ComponentModel.DataAnnotations;

namespace Scheduler.Application.Clients.Dtos;

public class CreateClientDto
{
    public CreateClientDto(string name)
    {
        Name = name;
    }

    [Required, StringLength(maximumLength: Client.MaxNameLength)]
    public string Name { get; protected set; }
}
