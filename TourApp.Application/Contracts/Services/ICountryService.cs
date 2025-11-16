namespace TourApp.Application.Contracts.Services;

public interface ICountryService
{
    public bool IsCountryAvailable(string countryName);
    public string? GetCountryDialCode(string countryName);
}