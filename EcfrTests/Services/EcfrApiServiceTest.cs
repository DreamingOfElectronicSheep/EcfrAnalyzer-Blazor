using EcfrAnalyzer.Services;
using EcfrTests.Builders;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;
using Xunit;

namespace EcfrTests.Services
{
    public class EcfrApiServiceTest
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly HttpClient _httpClient;
        private readonly EcfrApiService _service;

        public EcfrApiServiceTest()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object)
            {
                BaseAddress = new System.Uri("https://www.ecfr.gov/api/")
            };
            _service = new EcfrApiService(_httpClient);
        }

        #region GetAgenciesAsync Tests

        [Fact]
        public async Task GetAgenciesAsync_WithValidResponse_ReturnsAgenciesResponseModel()
        {
            var agenciesData = AgenciyResponseModelBuilder.CreateValidAgenciesResponse();
            var jsonContent = JsonSerializer.Serialize(agenciesData);
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json")
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponseMessage);

            var result = await _service.GetAgenciesAsync();

            Assert.NotNull(result);
            _mockHttpMessageHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.ToString().Contains("admin/v1/agencies.json")),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task GetAgenciesAsync_WithCachedData_ReturnsCachedResult()
        {
            var agenciesData = AgenciyResponseModelBuilder.CreateValidAgenciesResponse();
            var jsonContent = JsonSerializer.Serialize(agenciesData);
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json")
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponseMessage);

            var firstResult = await _service.GetAgenciesAsync();
            var secondResult = await _service.GetAgenciesAsync();

            _mockHttpMessageHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task GetAgenciesAsync_WithHttpRequestException_ReturnsEmptyResponse()
        {
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException("Network error"));

            var result = await _service.GetAgenciesAsync();

            Assert.NotNull(result);
            Assert.NotNull(result.Agencies);  
            Assert.Empty(result.Agencies);
        }

        [Fact]
        public async Task GetAgenciesAsync_WithJsonException_ReturnsEmptyResponse()
        {
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("invalid json", System.Text.Encoding.UTF8, "application/json")
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponseMessage);

            var result = await _service.GetAgenciesAsync();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAgenciesAsync_WithTaskCanceledException_ReturnsEmptyResponse()
        {
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new TaskCanceledException("Request timeout"));

            var result = await _service.GetAgenciesAsync();

            Assert.NotNull(result);
        }

        #endregion

        #region GetCorrectionsAsync Tests

        [Fact]
        public async Task GetCorrectionsAsync_WithValidResponse_ReturnsCorrectionsResponseModel()
        {
            var correctionsData = AgenciyResponseModelBuilder.CreateValidCorrectionsResponse();
            var jsonContent = JsonSerializer.Serialize(correctionsData);
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json")
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponseMessage);

            var result = await _service.GetCorrectionsAsync();

            Assert.NotNull(result);
            _mockHttpMessageHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.ToString().Contains("admin/v1/corrections.json")),
                ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task GetCorrectionsAsync_WithCachedData_ReturnsCachedResult()
        {
            var correctionsData = AgenciyResponseModelBuilder.CreateValidCorrectionsResponse();
            var jsonContent = JsonSerializer.Serialize(correctionsData);
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json")
            };

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponseMessage);

            var firstResult = await _service.GetCorrectionsAsync();
            var secondResult = await _service.GetCorrectionsAsync();

            _mockHttpMessageHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>());
        }

        #endregion
    }
}