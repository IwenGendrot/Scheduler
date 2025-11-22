namespace Scheduler.Application.Clients.Dtos;

public class UpdateClientDto : CreateClientDto
{
    public UpdateClientDto(string name) : base(name)
    { }
}
