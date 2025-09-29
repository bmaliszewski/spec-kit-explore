using Xunit;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using eFitnessKiosk.Services;
using System.Net;
using System.Text.Json;
using System.Text;

namespace eFitnessKiosk.Tests;

public class EFitnessAuthServiceTests
{
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly EFitnessAuthService _authService;

    public EFitnessAuthServiceTests()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_mockHttpMessageHandler.Object);

        var inMemorySettings = new Dictionary<string, string> {
            {"EFitnessApi:BaseUrl", "http://test.efitness.api/"}
        };
        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _authService = new EFitnessAuthService(_httpClient, _configuration);
    }

    [Fact]
    public async Task Login_ReturnsTokenOnSuccess()
    {
        // Arrange
        var email = "test@example.com";
        var password = "password";
        var expectedToken = "mocked_token";
        var authResponse = new AuthResponse { Token = expectedToken };

        _mockHttpMessageHandler.SetupSequence(h => h.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(authResponse), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _authService.Login(email, password);

        // Assert
        Assert.Equal(expectedToken, result);
    }

    [Fact]
    public async Task Register_ReturnsSuccess()
    {
        // Arrange
        var user = new Models.User { FirstName = "Test", LastName = "User", Email = "register@example.com" };
        var password = "password";

        _mockHttpMessageHandler.SetupSequence(h => h.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
            });

        // Act
        await _authService.Register(user, password);

        // Assert (no exception means success)
    }
}
