namespace Scheduler.Application.Patients.Dtos;

public class UpdatePatientDto : CreatePatientDto
{
    public UpdatePatientDto(Guid clientId, string name) : base(clientId, name)
    { }
}
