using Microsoft.AspNetCore.Mvc;
using eFitnessKiosk.Services;

namespace eFitnessKiosk.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PassesController : ControllerBase
{
    private readonly EFitnessPassService _passService;

    public PassesController(EFitnessPassService passService)
    {
        _passService = passService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAvailablePasses()
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var passes = await _passService.GetAvailablePasses(token);
        return Ok(passes);
    }
}
