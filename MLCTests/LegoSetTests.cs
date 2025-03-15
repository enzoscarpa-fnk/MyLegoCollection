using Bunit;
using Xunit;
using Moq;
using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection;
using MyLegoCollection.Pages;
using MyLegoCollection.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRebrickableService
{
    Task<List<LegoSet>> GetSetsAsync();
    Task<List<LegoSet>> SearchSetsAsync(string query);
}

public class FakeRebrickableService : IRebrickableService
{
    public Task<List<LegoSet>> GetSetsAsync()
    {
        return Task.FromResult(new List<LegoSet>
        {
            new LegoSet { SetNum = "1234-1", Name = "Test Set 1" },
            new LegoSet { SetNum = "1234-2", Name = "Test Set 2" },
            new LegoSet { SetNum = "1234-3", Name = "Test Set 3" },
            new LegoSet { SetNum = "1234-4", Name = "Test Set 4" },
            new LegoSet { SetNum = "1234-5", Name = "Test Set 5" },
            new LegoSet { SetNum = "1234-6", Name = "Test Set 6" }
        });
    }

    public Task<List<LegoSet>> SearchSetsAsync(string query)
    {
        return Task.FromResult(new List<LegoSet>
        {
            new LegoSet { SetNum = "1234-7", Name = "Test Set" }
        });
    }
}


public class SearchComponentTests : TestContext
{
    [Fact]
    public async Task Pagination_ShouldNavigateToNextPage()
    {
        // Arrange
        var fakeRebrickableService = new FakeRebrickableService();
        var mockCollectionService = new Mock<CollectionService>(Mock.Of<ILocalStorageService>());

		Services.AddSingleton<IRebrickableService>(fakeRebrickableService);
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
        var fakeRebrickableService = new FakeRebrickableService();
        var mockCollectionService = new Mock<CollectionService>(Mock.Of<ILocalStorageService>());

        Services.AddSingleton<IRebrickableService>(fakeRebrickableService);
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