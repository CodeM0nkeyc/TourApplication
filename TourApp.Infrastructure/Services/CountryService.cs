namespace TourApp.Infrastructure.Services;

public class CountryService : ICountryService
{
    private readonly ILogger<CountryService> _logger;
    private const string _countryApiUrl = "https://countriesnow.space/api/v0.1/countries";

    private Dictionary<string, string> _countriesWithDialCodes = null!;

    public CountryService(ILogger<CountryService> logger)
    {
        _logger = logger;
        LoadCountriesWithDialCodesAsync().Wait();
    }

    private async Task LoadCountriesWithDialCodesAsync()
    {
        using HttpClient client = new HttpClient();

        Dictionary<string, string> result;

        try
        {
            string countriesResponse = await client.GetStringAsync($"{_countryApiUrl}/codes");
            _countriesWithDialCodes = ParseCountriesWithDialCodesJson(countriesResponse);
        }
        catch
        {
            _logger.LogError("Failed to load countries.");
            _countriesWithDialCodes = new Dictionary<string, string>();
        }
    }

    private Dictionary<string, string> ParseCountriesWithDialCodesJson(string jsonResponse)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();
        
        using JsonDocument jsonDocument = JsonDocument.Parse(jsonResponse);
        
        JsonElement root = jsonDocument.RootElement;
        JsonElement dataElement = root.GetProperty("data");

        foreach (JsonElement element in dataElement.EnumerateArray())
        {
            string country = element.GetProperty("name").GetString()!;
            string dialCode = element.GetProperty("dial_code").GetString()!;
            
            result.Add(country, dialCode);
        }

        return result;
    }

    public bool IsCountryAvailable(string countryName)
    {
        return _countriesWithDialCodes.ContainsKey(countryName);
    }

    public string? GetCountryDialCode(string countryName)
    {
        _countriesWithDialCodes.TryGetValue(countryName, out string? result);
        return result;
    }
}