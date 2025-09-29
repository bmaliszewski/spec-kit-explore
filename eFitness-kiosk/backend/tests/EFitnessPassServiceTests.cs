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
using System.Collections.Generic;

namespace eFitnessKiosk.Tests;

public class EFitnessPassServiceTests
{
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly EFitnessPassService _passService;

    public EFitnessPassServiceTests()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_mockHttpMessageHandler.Object);

        var inMemorySettings = new Dictionary<string, string> {
            {"EFitnessApi:BaseUrl", "http://test.efitness.api/"}
        };
        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _passService = new EFitnessPassService(_httpClient, _configuration);
    }

    [Fact]
    public async Task GetAvailablePasses_ReturnsPassesOnSuccess()
    {
        // Arrange
        var token = "mocked_token";
        var expectedPasses = new List<Pass>
        {
            new Pass { Id = "1", Name = "Karnet Miesięczny", Price = 100, ValidityPeriod = "30 dni" },
            new Pass { Id = "2", Name = "Karnet Roczny", Price = 1000, ValidityPeriod = "365 dni" }
        };

        _mockHttpMessageHandler.SetupSequence(h => h.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(expectedPasses), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _passService.GetAvailablePasses(token);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedPasses.Count, result.Count());
    }
}
