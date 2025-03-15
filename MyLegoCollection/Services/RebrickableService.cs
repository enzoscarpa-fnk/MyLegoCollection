using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using MyLegoCollection.Models;

namespace MyLegoCollection.Models
{
    // Service to interact with the Rebrickable API for fetching LEGO sets
    public class RebrickableService
{
    private readonly HttpClient _httpClient; // HttpClient to make API requests
    private readonly string _apiKey; // API Key for authenticating requests

    // Constructor to initialize the service with HttpClient and API key
    public RebrickableService(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
    }

    // Fetches all available LEGO sets from the Rebrickable API
    public async Task<List<LegoSet>> GetSetsAsync()
    {
        try
        {
            // API call to fetch LEGO sets
            var response = await _httpClient.GetAsync($"https://rebrickable.com/api/v3/lego/sets/?key={_apiKey}");

            // Check if the response is successful
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"API error : {response.StatusCode}");
                return new List<LegoSet>(); // Return an empty list in case of failure
            }

            // Parse the response content into a LegoSetResponse object
            var legoResponse = await response.Content.ReadFromJsonAsync<LegoSetResponse>();

            return legoResponse?.Results ?? new List<LegoSet>(); // Return the set list or an empty list
        }
        catch (Exception ex)
        {
            // Log any errors encountered during the API request
            Console.WriteLine($"Error fetching sets: {ex.Message}");
            return new List<LegoSet>(); // Return an empty list in case of exception
        }
    }
    
    // Fetches the latest LEGO sets from the Rebrickable API, ordered by year
    public async Task<List<LegoSet>> GetLatestSetsAsync()
    {
        try
        {
            // API call to fetch the latest LEGO sets
            var response = await _httpClient.GetAsync($"https://rebrickable.com/api/v3/lego/sets/?ordering=-year&key={_apiKey}");

            // Check if the response is successful
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"API error : {response.StatusCode}");
                return new List<LegoSet>(); // Return an empty list in case of failure
            }

            // Parse the response content into a LegoSetResponse object
            var legoResponse = await response.Content.ReadFromJsonAsync<LegoSetResponse>();

            return legoResponse?.Results ?? new List<LegoSet>(); // Return the set list or an empty list
        }
        catch (Exception ex)
        {
            // Log any errors encountered during the API request
            Console.WriteLine($"Error fetching sets: {ex.Message}");
            return new List<LegoSet>(); // Return an empty list in case of exception
        }
    }
    
    // Searches for LEGO sets based on a query string
    public async Task<List<LegoSet>> SearchSetsAsync(string query)
    {
        try
        {
            // API call to search for LEGO sets using the query
            var response = await _httpClient.GetAsync($"https://rebrickable.com/api/v3/lego/sets/?search={query}&key={_apiKey}");

            // Check if the response is successful
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"API error : {response.StatusCode}");
                return new List<LegoSet>(); // Return an empty list in case of failure
            }

            // Parse the response content into a LegoSetResponse object
            var legoResponse = await response.Content.ReadFromJsonAsync<LegoSetResponse>();

            return legoResponse?.Results ?? new List<LegoSet>(); // Return the set list or an empty list
        }
        catch (Exception ex)
        {
            // Log any errors encountered during the API request
            Console.WriteLine($"Error searching sets: {ex.Message}");
            return new List<LegoSet>(); // Return an empty list in case of exception
        }
    }
}
}