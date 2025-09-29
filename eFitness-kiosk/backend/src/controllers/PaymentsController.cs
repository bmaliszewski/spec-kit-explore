using Microsoft.AspNetCore.Mvc;
using eFitnessKiosk.Services;

namespace eFitnessKiosk.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly EFitnessPaymentService _paymentService;

    public PaymentsController(EFitnessPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("Passes/{passId}/Initiate")]
    public async Task<IActionResult> InitiatePayment(string passId, [FromBody] PaymentRequest request)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var redirectUrl = await _paymentService.InitiatePayment(token, passId, request.PaymentMethod);
        return Ok(new { RedirectUrl = redirectUrl });
    }

    [HttpGet("Me")]
    public async Task<IActionResult> GetPaymentHistory()
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var history = await _paymentService.GetPaymentHistory(token);
        return Ok(history);
    }
}

public class PaymentRequest
{
    public string PaymentMethod { get; set; }
}
