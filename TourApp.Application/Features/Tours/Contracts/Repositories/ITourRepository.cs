namespace TourApp.Application.Features.Tours.Contracts.Repositories;

public interface ITourRepository : IGenericRepository<Tour, int>
{
    public Task<IList<string>> GetTourCountriesAsync();
}