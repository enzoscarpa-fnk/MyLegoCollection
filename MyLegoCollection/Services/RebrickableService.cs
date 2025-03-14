using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using MyLegoCollection.Models;

public class RebrickableService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public RebrickableService(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
    }

    public async Task<List<LegoSet>> GetSetsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"https://rebrickable.com/api/v3/lego/sets/?key={_apiKey}");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"API error : {response.StatusCode}");
                return new List<LegoSet>();
            }

            var legoResponse = await response.Content.ReadFromJsonAsync<LegoSetResponse>();

            return legoResponse?.Results ?? new List<LegoSet>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching sets: {ex.Message}");
            return new List<LegoSet>();
        }
    }
    
    public async Task<List<LegoSet>> GetLatestSetsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"https://rebrickable.com/api/v3/lego/sets/?ordering=-year&key={_apiKey}");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"API error : {response.StatusCode}");
                return new List<LegoSet>();
            }

            var legoResponse = await response.Content.ReadFromJsonAsync<LegoSetResponse>();

            return legoResponse?.Results ?? new List<LegoSet>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching sets: {ex.Message}");
            return new List<LegoSet>();
        }
    }
    
    public async Task<List<LegoSet>> SearchSetsAsync(string query)
    {
        try
        {
            var response = await _httpClient.GetAsync($"https://rebrickable.com/api/v3/lego/sets/?search={query}&key={_apiKey}");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"API error : {response.StatusCode}");
                return new List<LegoSet>();
            }

            var legoResponse = await response.Content.ReadFromJsonAsync<LegoSetResponse>();

            return legoResponse?.Results ?? new List<LegoSet>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error searching sets: {ex.Message}");
            return new List<LegoSet>();
        }
    }
}