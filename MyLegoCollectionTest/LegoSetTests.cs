using bUnit;
using Xunit;
using Moq;
using MyLegoCollection.Pages;
using MyLegoCollection.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SearchComponentTests : TestContext
{
    [Fact]
    public void Pagination_ShouldGoToNextPage()
    {
        // Arrange
        var mockRebrickableService = new Mock<RebrickableService>();
        mockRebrickableService.Setup(s => s.GetSetsAsync())
            .ReturnsAsync(new List<LegoSet>
            {
                new LegoSet { SetNum = "1234-1", Name = "Test Set 1" },
                new LegoSet { SetNum = "1234-2", Name = "Test Set 2" },
                new LegoSet { SetNum = "1234-3", Name = "Test Set 3" },
                new LegoSet { SetNum = "1234-4", Name = "Test Set 4" },
                new LegoSet { SetNum = "1234-5", Name = "Test Set 5" }
            });

        var mockCollectionService = new Mock<CollectionService>(null);
        var component = RenderComponent<Search>(
            parameters => parameters
                .AddDependency(mockRebrickableService.Object)
                .AddDependency(mockCollectionService.Object)
        );

        // Act : Trouver et cliquer sur le bouton "Next" pour aller à la page suivante
        var nextButton = component.Find("button.next");
        nextButton.Click();

        // Assert : Vérifie que la page a bien changé
        var currentPage = component.Find("span.current-page");
        Assert.Contains("Page 2", currentPage.TextContent);
    }

    [Fact]
    public async Task SearchSets_ShouldUpdateResults()
    {
        // Arrange
        var mockRebrickableService = new Mock<RebrickableService>();
        mockRebrickableService.Setup(s => s.SearchSetsAsync("Test"))
            .ReturnsAsync(new List<LegoSet>
            {
                new LegoSet { SetNum = "1234-1", Name = "Test Set", Year = 2023 }
            });

        var mockCollectionService = new Mock<CollectionService>(null);

        var component = RenderComponent<Search>(
            parameters => parameters
                .AddDependency(mockRebrickableService.Object)
                .AddDependency(mockCollectionService.Object)
        );

        // Act : Simuler une recherche
        var input = component.Find("input");
        input.Change("Test");

        var searchButton = component.Find("button");
        searchButton.Click();

        // Assert : Vérifie que les résultats de recherche sont bien affichés
        var result = component.Find("ul");
        Assert.Contains("Test Set", result.TextContent);
    }
}