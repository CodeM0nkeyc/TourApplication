namespace TourApp.Application.Features.Tours.Contracts.Repositories;

public interface ITourRepository : IGenericRepository<Tour>
{
    public Task<IList<string>> GetTourCountriesAsync();
}