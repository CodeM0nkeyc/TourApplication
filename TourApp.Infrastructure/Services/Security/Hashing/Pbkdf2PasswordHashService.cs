namespace TourApp.Infrastructure.Services.Security.Hashing;

public class Pbkdf2PasswordHashService : IPasswordHashService
{
    private const int _iterations = 600000;
    private const int _hashLength = 32;
    
    private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA256;
    
    public byte[] ComputeHash(byte[] password, byte[] salt)
    {
        return Rfc2898DeriveBytes.Pbkdf2(password, salt, _iterations, _hashAlgorithm, _hashLength);
    }

    public byte[] GenerateSalt()
    {
        return RandomNumberGenerator.GetBytes(_hashLength);
    }
}