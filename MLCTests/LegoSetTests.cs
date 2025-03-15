using Bunit;
using Xunit;
using Moq;
using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection;
using MyLegoCollection.Pages;
using MyLegoCollection.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SearchComponentTests : TestContext
{
    [Fact]
    public void Pagination_ShouldNavigateToNextPage()
    {
        // Arrange
        var mockHttpClient = new Mock<HttpClient>();
        var mockRebrickableService = new Mock<RebrickableService>(mockHttpClient.Object, "apiKey");
        mockRebrickableService.Setup(s => s.GetSetsAsync())
            .ReturnsAsync(new List<LegoSet>
            {
                new LegoSet { SetNum = "1234-1", Name = "Test Set 1" },
                new LegoSet { SetNum = "1234-2", Name = "Test Set 2" },
                new LegoSet { SetNum = "1234-3", Name = "Test Set 3" },
                new LegoSet { SetNum = "1234-4", Name = "Test Set 4" },
                new LegoSet { SetNum = "1234-5", Name = "Test Set 5" },
                new LegoSet { SetNum = "1234-6", Name = "Test Set 6" }
            });

        var mockCollectionService = new Mock<CollectionService>(Mock.Of<ILocalStorageService>());

        Services.AddSingleton(mockRebrickableService.Object);
        Services.AddSingleton(mockCollectionService.Object);

        var component = RenderComponent<Search>();

        // Act : Click on NEXT button
        var nextButton = component.Find("button.next");
        nextButton.Click();

        // Assert : Ensure page has changed
        var paginationText = component.Find("span").TextContent;
        Assert.Contains("Page 2", paginationText);
    }

    [Fact]
    public async Task Search_ShouldDisplayResultsBasedOnQuery()
    {
        // Arrange
        var mockHttpClient = new Mock<HttpClient>();
        var mockRebrickableService = new Mock<RebrickableService>(mockHttpClient.Object, "apiKey");
        mockRebrickableService.Setup(s => s.SearchSetsAsync("Test"))
            .ReturnsAsync(new List<LegoSet>
            {
                new LegoSet { SetNum = "1234-7", Name = "Test Set" }
            });

        var mockCollectionService = new Mock<CollectionService>(Mock.Of<ILocalStorageService>());

        Services.AddSingleton(mockRebrickableService.Object);
        Services.AddSingleton(mockCollectionService.Object);

        var component = RenderComponent<Search>();

        // Act : Simulates a search query
        var input = component.Find("input");
        input.Change("Test");

        var searchButton = component.Find("button");
        searchButton.Click();

        // Assert : Ensure correct result is displayed
        var result = component.Find("ul").TextContent;
        Assert.Contains("Test Set", result);
    }
}