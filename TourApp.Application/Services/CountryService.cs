using System.Text.Json;

namespace TourApp.Application.Services;

public class CountryService
{
    private const string _countryApiUrl = "https://countriesnow.space/api/v0.1/countries";

    private Dictionary<string, string> _countriesWithDialCodes = null!;

    public CountryService()
    {
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

    public static async Task<CountryService> CreateAsync()
    {
        CountryService service = new CountryService();
        await service.LoadCountriesWithDialCodesAsync();
        
        return service;
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