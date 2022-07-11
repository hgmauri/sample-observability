using Microsoft.AspNetCore.Mvc;
using Sample.Observability.Infrastructure.ViewModels;

namespace Sample.Observability.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostClient([FromBody] ClientViewModel client)
    {
        Serilog.Log.Information($"PostClient method called: {client.Name}");

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetClient([FromQuery] string name)
    {
        Serilog.Log.Information($"GetClient method called. With the parameter name={name}");

        return Ok();
    }
}