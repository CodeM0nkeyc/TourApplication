namespace TourApp.Infrastructure.Services.Security.Generator;

public class ConfirmationGenerator : IConfirmationGenerator
{
    public int GenerateCode()
    {
        int lowerBound = (int)Math.Pow(10, 6);
        int upperBound = (int)Math.Pow(10, 7);
        
        int code = RandomNumberGenerator.GetInt32(lowerBound, upperBound);

        return code;
    }
}