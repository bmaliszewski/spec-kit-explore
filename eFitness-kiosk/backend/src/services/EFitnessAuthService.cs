using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using eFitnessKiosk.Models;

namespace eFitnessKiosk.Services;

public class EFitnessAuthService
{
    private readonly HttpClient _httpClient;
    private readonly string _eFitnessApiBaseUrl;

    public EFitnessAuthService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _eFitnessApiBaseUrl = configuration["EFitnessApi:BaseUrl"];
        _httpClient.BaseAddress = new Uri(_eFitnessApiBaseUrl);
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<string> Login(string email, string password)
    {
        var loginRequest = new
        {
            Login = email,
            Password = password
        };
        var jsonContent = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/Auth/Login", jsonContent);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var authResponse = JsonSerializer.Deserialize<AuthResponse>(responseContent);

        return authResponse.Token;
    }

    public async Task Register(User user, string password)
    {
        var registerRequest = new
        {
            user.FirstName,
            user.LastName,
            user.Email,
            Password = password,
            user.PersonalData,
            user.RodoConsents
        };
        var jsonContent = new StringContent(JsonSerializer.Serialize(registerRequest), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/Auth/Register", jsonContent);
        response.EnsureSuccessStatusCode();
    }
}

public class AuthResponse
{
    public string Token { get; set; }
}
