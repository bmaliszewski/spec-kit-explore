using Microsoft.AspNetCore.Mvc;
using eFitnessKiosk.Services;
using eFitnessKiosk.Models;

namespace eFitnessKiosk.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly EFitnessUserService _userService;

    public UsersController(EFitnessUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("Me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        // In a real application, you would get the token from the request header
        // For simplicity, we'll assume a token is passed in the query string for now
        var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var user = await _userService.GetCurrentUser(token);
        return Ok(user);
    }

    [HttpPut("Me")]
    public async Task<IActionResult> UpdateCurrentUser([FromBody] User user)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        await _userService.UpdateUser(token, user);
        return Ok();
    }
}
