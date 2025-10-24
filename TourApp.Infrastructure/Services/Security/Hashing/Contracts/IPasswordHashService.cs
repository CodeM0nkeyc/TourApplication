namespace TourApp.Infrastructure.Services.Security.Hashing.Contracts;

public interface IPasswordHashService
{
    public byte[] ComputeHash(byte[] password, byte[] salt);
    public byte[] GenerateSalt();
}