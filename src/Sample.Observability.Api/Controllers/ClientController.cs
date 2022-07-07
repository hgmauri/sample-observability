using Microsoft.AspNetCore.Mvc;
using Sample.Observability.Infrastructure.Events;

namespace Sample.Observability.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly ILogger<ClientController> _logger;

    public ClientController(ILogger<ClientController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> PostClient([FromBody] ClientSavedEvent client)
    {
        _logger.LogInformation($"Send client: {client.Name}");

        return Ok();
    }
}