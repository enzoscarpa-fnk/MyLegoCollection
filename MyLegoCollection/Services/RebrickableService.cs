using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

public class LegoSet
{
    public string set_num { get; set; }
    public string name { get; set; }
    public int year { get; set; }
    public int num_parts { get; set; }
    public string set_img_url { get; set; }
}

public class LegoSetResponse
{
    public List<LegoSet> results { get; set; }
}

public class RebrickableService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "de835dfe59784d5b5bcf67850a3e544e";

    public RebrickableService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<LegoSet>> GetSetsAsync()
    {
        var response = await _httpClient.GetAsync($"https://rebrickable.com/api/v3/lego/sets/?key={_apiKey}");
        
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"API error : {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            return new List<LegoSet>();
        }

        var json = await response.Content.ReadAsStringAsync();

        // Log pour voir la réponse brute
        Console.WriteLine($"API response : {json}");

        try
        {
            var legoResponse = JsonSerializer.Deserialize<LegoSetResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return legoResponse?.results ?? new List<LegoSet>();
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON parsing error : {ex.Message}");
            return new List<LegoSet>();
        }
    }
}