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

public class EFitnessPaymentServiceTests
{
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly EFitnessPaymentService _paymentService;

    public EFitnessPaymentServiceTests()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_mockHttpMessageHandler.Object);

        var inMemorySettings = new Dictionary<string, string> {
            {"EFitnessApi:BaseUrl", "http://test.efitness.api/"}
        };
        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _paymentService = new EFitnessPaymentService(_httpClient, _configuration);
    }

    [Fact]
    public async Task InitiatePayment_ReturnsRedirectUrlOnSuccess()
    {
        // Arrange
        var token = "mocked_token";
        var passId = "pass123";
        var paymentMethod = "card";
        var expectedRedirectUrl = "http://mock.payu.com/redirect";
        var paymentInitiateResponse = new PaymentInitiateResponse { RedirectUrl = expectedRedirectUrl };

        _mockHttpMessageHandler.SetupSequence(h => h.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(paymentInitiateResponse), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _paymentService.InitiatePayment(token, passId, paymentMethod);

        // Assert
        Assert.Equal(expectedRedirectUrl, result);
    }

    [Fact]
    public async Task GetPaymentHistory_ReturnsPaymentHistoryOnSuccess()
    {
        // Arrange
        var token = "mocked_token";
        var expectedPaymentHistory = new List<Payment>
        {
            new Payment { Id = "pay1", Amount = 100, Status = "Completed" },
            new Payment { Id = "pay2", Amount = 50, Status = "Pending" }
        };

        _mockHttpMessageHandler.SetupSequence(h => h.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(expectedPaymentHistory), Encoding.UTF8, "application/json")
            });

        // Act
        var result = await _paymentService.GetPaymentHistory(token);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedPaymentHistory.Count, result.Count());
    }
}
