using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using eFitnessKiosk.Models;

namespace eFitnessKiosk.Services;

public class EFitnessUserService
{
    private readonly HttpClient _httpClient;
    private readonly string _eFitnessApiBaseUrl;

    public EFitnessUserService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _eFitnessApiBaseUrl = configuration["EFitnessApi:BaseUrl"];
        _httpClient.BaseAddress = new Uri(_eFitnessApiBaseUrl);
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<User> GetCurrentUser(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync("api/User/Current");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<User>(responseContent);
    }

    public async Task UpdateUser(string token, User user)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var jsonContent = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"api/User/{user.Id}", jsonContent);
        response.EnsureSuccessStatusCode();
    }
}
