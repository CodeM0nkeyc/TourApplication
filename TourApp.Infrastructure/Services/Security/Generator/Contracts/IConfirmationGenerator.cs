namespace TourApp.Infrastructure.Services.Security.Generator.Contracts;

public interface IConfirmationGenerator
{
    public int GenerateCode();
    public string GenerateUrlToken();
}