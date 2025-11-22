using Microsoft.AspNetCore.Mvc;
using Scheduler.Application.Appointements;
using Scheduler.Application.Appointements.Dtos;
using Scheduler.Application.Clients;
using Scheduler.Application.Clients.Dtos;
using Scheduler.Application.Patients;
using Scheduler.Application.Patients.Dtos;

namespace Scheduler.Api.Controllers;

[ApiController]
[Route("test")]
public class TestSetupController(IAppointementService appointementService, IClientService clientService, IPatientService patientService) : ControllerBase
{
    private readonly IAppointementService _appointementService = appointementService;
    private readonly IClientService _clientService = clientService;
    private readonly IPatientService _patientService = patientService;

    [HttpGet]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        Client clientOne = _clientService.Create(new CreateClientDto("ClientOne"));
        Client clientTwo = _clientService.Create(new CreateClientDto("ClientTwo"));

        Patient patientOne = _patientService.Create(new CreatePatientDto(clientOne.Id, "PatientOne"));
        Patient patientOther = _patientService.Create(new CreatePatientDto(clientOne.Id, "PatientOther"));
        Patient patientTwo = _patientService.Create(new CreatePatientDto(clientTwo.Id, "PatientTwo"));

        Appointement appointementOne = _appointementService.Create(new CreateAppointementDto(clientOne.Id, patientOne.Id, new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1), 11));

        return Ok($"""
            Created 2 clients, 3 patients and 1 appointement:
            Clients [
                {clientOne.Id} - {clientOne.Name},
                {clientTwo.Id} - {clientTwo.Name},
            ],
            Patients [
                {patientOne.Id} - {patientOne.Name} for client {patientOne.ClientId},
                {patientOther.Id} - {patientOther.Name} for client {patientOther.ClientId},
                {patientTwo.Id} - {patientTwo.Name} for client {patientTwo.ClientId},
            ]
            Appointements [
                {appointementOne.Id} - client {appointementOne.ClientId} - patient {appointementOne.PatientId} - {appointementOne.AppointementDate} - {appointementOne.Hour}
            ]
            """);
    }
}
