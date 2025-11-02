namespace TourApp.Application.Models.Result;

public record Error(string Code, string? Description = null)
{
    public static Error None = new Error(string.Empty);
}