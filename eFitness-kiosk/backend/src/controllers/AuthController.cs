using Microsoft.AspNetCore.Mvc;
using eFitnessKiosk.Services;

namespace eFitnessKiosk.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly EFitnessAuthService _authService;

    public AuthController(EFitnessAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _authService.Login(request.Email, request.Password);
        return Ok(new { Token = token });
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        // In a real application, you would map RegisterRequest to a User model
        // and handle RODO consents appropriately.
        // For simplicity, we'll just pass the data to the service.
        var user = new Models.User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PersonalData = request.PersonalData,
            RodoConsents = request.RodoConsents
        };
        await _authService.Register(user, request.Password);
        return Ok();
    }
}

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class RegisterRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public object PersonalData { get; set; }
    public object[] RodoConsents { get; set; }
}
