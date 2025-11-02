namespace TourApp.Application.Features.Users.Validators;

public class RegistrationValidator : AbstractValidator<RegistrationRequest>
{
    public RegistrationValidator(CountryService countryService)
    {
        string codePrefix = RegistrationErrors.Prefix;

        RuleFor(x => x.Email).EmailAddress()
            .WithCodeAndMessage($"{codePrefix}.Email", "Email is incorrect");
        
        RuleFor(x => x.PhoneNumber!).PhoneNumber($"{codePrefix}.PhoneNumber")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

        RuleFor(x => x.FirstName).IsWord()
            .WithCodeAndMessage($"{codePrefix}.FirstName", "Only letters allowed");
        
        RuleFor(x => x.LastName).IsWord()
            .WithCodeAndMessage($"{codePrefix}.FirstName", "Only letters allowed");

        RuleFor(x => x.MiddleName!).IsWord()
            .WithCodeAndMessage($"{codePrefix}.MiddleName", "Only letters allowed")
            .When(x => !string.IsNullOrWhiteSpace(x.MiddleName));
        
        RuleFor(x => x.Address.Country).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithCodeAndMessage($"{codePrefix}.AddressCountry", "Country is required")
            .Must(countryService.IsCountryAvailable)
            .WithCodeAndMessage($"{codePrefix}.AddressCountry", "{PropertyValue} is not available");

        RuleFor(x => x.Password).Password($"{codePrefix}.Password");
    }
}