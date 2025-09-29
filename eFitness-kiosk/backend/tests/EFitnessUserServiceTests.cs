using Xunit;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using eFitnessKiosk.Services;
using System.Net;
using System.Text.Json;
using System.Text;
using eFitnessKiosk.Models;

namespace eFitnessKiosk.Tests;

public class EFitnessUserServiceTests
{
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly EFitnessUserService _userService;

    public EFitnessUserServiceTests()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_mockHttpMessageHandler.Object);

        var inMemorySettings = new Dictionary<string, string> {
            {"EFitnessApi:BaseUrl", "http://test.efitness.api/"}
        };
        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _userService = new EFitnessUserService(_httpClient, _configuration);
    }

    [Fact]
    public async Task GetCurrentUser_ReturnsUserOnSuccess()
    {
        // Arrange
        var token = "mocked_token";
        var expectedUser = new User { Id = "1", FirstName = "Test", LastName = "User", Email = "test@example.com" };

        _mockHttpMessageHandler.SetupSequence(h => h.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(expectedUser), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _userService.GetCurrentUser(token);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedUser.Id, result.Id);
    }

    [Fact]
    public async Task UpdateUser_ReturnsSuccess()
    {
        // Arrange
        var token = "mocked_token";
        var userToUpdate = new User { Id = "1", FirstName = "Updated", LastName = "User", Email = "updated@example.com" };

        _mockHttpMessageHandler.SetupSequence(h => h.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
            });

        // Act
        await _userService.UpdateUser(token, userToUpdate);

        // Assert (no exception means success)
    }
}
