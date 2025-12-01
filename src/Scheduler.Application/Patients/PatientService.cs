using Scheduler.Application.Patients.Dtos;
using Scheduler.Data.Clients.Repositories;
using Scheduler.Data.Patients.Repositories;

namespace Scheduler.Application.Patients;

public class PatientService(IClientRepository clientRepository, IPatientRepository patientRepository) : IPatientService
{
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IPatientRepository _patientRepository = patientRepository;

    public Patient Create(CreatePatientDto dto)
    {
        return _patientRepository.Create(dto.ClientId, dto.Name);
    }

    public IReadOnlyCollection<Patient> GetAll()
    {
        return _patientRepository.GetAll();
    }

    public Patient Get(Guid patientId)
    {
        return _patientRepository.Get(patientId);
    }

    public Patient Update(Guid id, UpdatePatientDto dto)
    {
        _clientRepository.Get(dto.ClientId);
        return _patientRepository.Update(id, dto.ClientId, dto.Name);
    }
}
