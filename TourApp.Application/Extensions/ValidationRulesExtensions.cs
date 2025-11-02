namespace TourApp.Application.Extensions;

public static class ValidationRulesExtensions
{
    private const string _passwordPattern = 
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&_])[A-Za-z\d@$!%*?&_]{8,}$";

    private const string _wordPattern = @"^[A-Za-z\s]+$";
    
    private const string _phoneNumberPattern = @"^\d{3}\s?\d{3}\s?\d{2}\s?\d{2}$";
    
    public static IRuleBuilderOptions<TEntity, TProp> WithCodeAndMessage<TEntity, TProp>(
        this IRuleBuilderOptions<TEntity, TProp> ruleBuilder, string errorCode, string errorMessage)
    {
        return ruleBuilder.WithErrorCode(errorCode).WithMessage(errorMessage);
    }
    
    
    public static IRuleBuilderOptions<TEntity, string> Password<TEntity>(
        this IRuleBuilder<TEntity, string> ruleBuilder, string errorCode)
    {
        return ruleBuilder.Matches(_passwordPattern)
            .WithCodeAndMessage(errorCode,
                "Password must have at least 8 characters, where one uppercase, lowercase, " +
                "special symbol (@$!%*?&_) and number must be included");
    }
    
    
    public static IRuleBuilderOptions<TEntity, string> IsWord<TEntity>(
        this IRuleBuilder<TEntity, string> ruleBuilder)
    {
        return ruleBuilder.Matches(_wordPattern);
    }
    
    
    public static IRuleBuilderOptions<TEntity, string> PhoneNumber<TEntity>(
        this IRuleBuilder<TEntity, string> ruleBuilder, string errorCode)
    {
        return ruleBuilder.Matches(_phoneNumberPattern)
            .WithCodeAndMessage(errorCode, "Phone number is incorrect");
    }
}