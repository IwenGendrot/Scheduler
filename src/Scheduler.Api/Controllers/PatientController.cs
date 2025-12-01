using Microsoft.AspNetCore.Mvc;
using Scheduler.Application.Patients;
using Scheduler.Application.Patients.Dtos;
using System.Net.Mime;

namespace Scheduler.Api.Controllers;

[ApiController]
[Route("patient")]
[Produces(MediaTypeNames.Application.Json)]
public class PatientController(IPatientService patientService) : ControllerBase
{
    private readonly IPatientService _patientService = patientService;

    /// <summary>
    /// Get a patient looking for its id
    /// </summary>
    /// <param name="patientId">Id to look for</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The patient</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType<Patient>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        try
        {
            return Ok(_patientService.Get(id));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Get all existing patients
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>All patients</returns>
    [HttpGet]
    [ProducesResponseType<IReadOnlyCollection<Patient>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(_patientService.GetAll());
    }


    /// <summary>
    /// Create a patient
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Newly created patient</returns>
    [HttpPost]
    [ProducesResponseType<Patient>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CreatePatientDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Patient patient = _patientService.Create(dto);
        return CreatedAtAction(nameof(Get), new { id = patient.Id }, patient);
    }

    /// <summary>
    /// Update a patient
    /// </summary>
    /// <param name="id">Patient's Id to update</param>
    /// <param name="dto">New patient values</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Updated patient</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType<Patient>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePatientDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            return Ok(_patientService.Update(id, dto));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
