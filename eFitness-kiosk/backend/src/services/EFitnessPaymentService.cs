using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using eFitnessKiosk.Models;

namespace eFitnessKiosk.Services;

public class EFitnessPaymentService
{
    private readonly HttpClient _httpClient;
    private readonly string _eFitnessApiBaseUrl;

    public EFitnessPaymentService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _eFitnessApiBaseUrl = configuration["EFitnessApi:BaseUrl"];
        _httpClient.BaseAddress = new Uri(_eFitnessApiBaseUrl);
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<string> InitiatePayment(string token, string passId, string paymentMethod)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var paymentRequest = new
        {
            PassId = passId,
            PaymentMethod = paymentMethod
        };
        var jsonContent = new StringContent(JsonSerializer.Serialize(paymentRequest), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"api/Payments/Passes/{passId}/Initiate", jsonContent);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var paymentResponse = JsonSerializer.Deserialize<PaymentInitiateResponse>(responseContent);

        return paymentResponse.RedirectUrl;
    }

    public async Task<IEnumerable<Payment>> GetPaymentHistory(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync("api/Payments/Me");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<Payment>>(responseContent);
    }
}

public class PaymentInitiateResponse
{
    public string RedirectUrl { get; set; }
}
