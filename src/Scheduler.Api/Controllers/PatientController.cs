using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Scheduler.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class PatientController : ControllerBase
{
}
