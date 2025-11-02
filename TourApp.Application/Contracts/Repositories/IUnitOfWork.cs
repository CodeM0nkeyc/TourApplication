namespace TourApp.Application.Contracts.Repositories;

public interface IUnitOfWork
{
    public Task SaveAsync();
}