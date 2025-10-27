namespace TourApp.Persistence.Repositories;

public class TourRepository : GenericRepository<Tour>, ITourRepository
{
    public TourRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IList<string>> GetTourCountriesAsync()
    {
        IList<string> result = await dbContext.Tours
            .Select(tour => tour.Country)
            .ToListAsync();
        
        return result;
    }
}