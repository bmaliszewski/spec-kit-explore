using System.Net.Http.Headers;
using System.Text.Json;
using eFitnessKiosk.Models;

namespace eFitnessKiosk.Services;

public class EFitnessPassService
{
    private readonly HttpClient _httpClient;
    private readonly string _eFitnessApiBaseUrl;

    public EFitnessPassService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _eFitnessApiBaseUrl = configuration["EFitnessApi:BaseUrl"];
        _httpClient.BaseAddress = new Uri(_eFitnessApiBaseUrl);
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<IEnumerable<Pass>> GetAvailablePasses(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.GetAsync("api/Passes");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<Pass>>(responseContent);
    }
}
