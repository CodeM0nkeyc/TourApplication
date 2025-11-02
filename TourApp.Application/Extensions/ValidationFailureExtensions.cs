namespace TourApp.Application.Extensions;

public static class ValidationFailureExtensions
{
    public static IEnumerable<Error> ToErrors(this List<ValidationFailure> failures)
    {
        return failures.Select(failure => new Error(failure.ErrorCode, failure.ErrorMessage));
    }
}