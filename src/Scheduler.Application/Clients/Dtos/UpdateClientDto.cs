using System.ComponentModel.DataAnnotations;

namespace Scheduler.Application.Clients.Dtos;

public class UpdateClientDto
{
    public UpdateClientDto(string name)
    {
        Name = name;
    }

    [Required, StringLength(maximumLength: Client.MaxNameLength)]
    public string Name { get; private set; }
}
