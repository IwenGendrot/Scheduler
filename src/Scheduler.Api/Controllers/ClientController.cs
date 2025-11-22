using Microsoft.AspNetCore.Mvc;
using Scheduler.Application.Clients;
using Scheduler.Application.Clients.Dtos;
using System.Net.Mime;

namespace Scheduler.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    /// <summary>
    /// Get a client looking for its id
    /// </summary>
    /// <param name="clientId">Id to look for</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The client</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType<Client>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        try
        {
            return Ok(_clientService.Get(id));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Get all existing client
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>All clients</returns>
    [HttpGet]
    [ProducesResponseType<IReadOnlyCollection<Client>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken) => Ok(_clientService.GetAll());

    /// <summary>
    /// Create a client
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Newly created client</returns>
    [HttpPost]
    [ProducesResponseType<Client>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateClientDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Client client = _clientService.Create(dto);
        return CreatedAtAction(nameof(Get), new { id = client.Id }, client);
    }

    /// <summary>
    /// Update a client
    /// </summary>
    /// <param name="id">Client's Id to update</param>
    /// <param name="dto">New client values</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Updated client</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType<Client>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateClientDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            return Ok(_clientService.Update(id, dto));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
