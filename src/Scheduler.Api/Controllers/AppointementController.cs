using Microsoft.AspNetCore.Mvc;
using Scheduler.Application.Appointements;
using Scheduler.Application.Appointements.Dtos;
using System.Net.Mime;

namespace Scheduler.Api.Controllers;

[ApiController]
[Route("appointement")]
[Produces(MediaTypeNames.Application.Json)]
public class AppointementController : ControllerBase
{
    private readonly IAppointementService _appointementService;

    public AppointementController(IAppointementService appointementService)
    {
        _appointementService = appointementService;
    }

    /// <summary>
    /// Get appointement from its id
    /// </summary>
    /// <param name="appointementId"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType<IReadOnlyCollection<Appointement>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        try
        {
            return Ok(_appointementService.Get(id));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Get available hours for given client on given date
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="date"></param>
    /// <returns></returns>
    [HttpGet("{clientId:guid}/{date:datetime}")]
    [ProducesResponseType<IReadOnlyCollection<int>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAvailableHoursOnDate([FromRoute] Guid clientId, [FromRoute] DateOnly date)
    {
        return Ok(_appointementService.GetAvailableHoursOnDate(clientId, date));
    }


    /// <summary>
    /// Get all appointements for a given clientId
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    [HttpGet("{clientId:guid}")]
    [ProducesResponseType<IReadOnlyCollection<Appointement>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetForClient([FromRoute] Guid clientId)
    {
        return Ok(_appointementService.GetForClient(clientId));
    }

    /// <summary>
    /// Get all appointements for a given patient id
    /// </summary>
    /// <param name="patientId"></param>
    /// <returns></returns>
    [HttpGet("{patientId:guid}")]
    [ProducesResponseType<IReadOnlyCollection<Appointement>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetForPatient([FromRoute] Guid patientId)
    {
        return Ok(_appointementService.GetForPatient(patientId));
    }

    /// <summary>
    /// Get all appointements for a given clientId and a given patientId
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="patientId"></param>
    /// <returns></returns>
    [HttpGet("{clientId:guid}:{patientId:guid}")]
    [ProducesResponseType<IReadOnlyCollection<Appointement>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetForClientAndPatient([FromRoute] Guid clientId, [FromRoute] Guid patientId)
    {
        return Ok(_appointementService.GetForClientAndPatient(clientId, patientId));
    }

    /// <summary>
    /// Get all appointements
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType<IReadOnlyCollection<Appointement>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(_appointementService.GetAll());
    }

    /// <summary>
    /// Create a appointement
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType<IReadOnlyCollection<Appointement>>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateAppointementDto dto)
    {
        CheckAppointementTime(dto.AppointementDate);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Appointement appointement = _appointementService.Create(dto);
        return CreatedAtAction(nameof(Get), new { id = appointement.Id }, appointement);
    }

    /// <summary>
    /// Update an appointement, only AppointementTime can be updated
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType<IReadOnlyCollection<Appointement>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateAppointementDto dto)
    {
        CheckAppointementTime(dto.AppointementDate);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Appointement appointement = _appointementService.Update(id, dto);
        return CreatedAtAction(nameof(Get), new { id = appointement.Id }, appointement);
    }


    private void CheckAppointementTime(DateOnly appointementDate)
    {
        if (Appointement.ClosedDays.Contains(appointementDate.DayOfWeek))
        {
            ModelState.AddModelError("Clinic is closed on Saturdays and Sundays", "Choose a weekday between Monday and Friday");
        }
    }
}
