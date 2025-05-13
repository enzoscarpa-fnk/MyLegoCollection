using Xunit;
using Moq;
using MyLegoCollection.Models;
using System.Net.Http.Json;
using System.Net;
using Moq.Protected;

public class RebrickableServiceTests
{
    [Fact]
    public async Task GetSetsAsync_ShouldReturnSets()
    {
        var fakeApiResponse = new LegoSetResponse
        {
            Results = new List<LegoSet>
            {
                new LegoSet { SetNum = "1234-1", Name = "Test Set", Year = 2023 }
            }
        };

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        
        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(fakeApiResponse)
            });

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var service = new RebrickableService(httpClient, "fake_api_key");

        var result = await service.GetSetsAsync();

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("1234-1", result[0].SetNum);
        Assert.Equal("Test Set", result[0].Name);
    }
}