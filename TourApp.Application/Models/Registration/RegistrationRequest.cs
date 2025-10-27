namespace TourApp.Application.Models.Registration;

public record RegistrationRequest(
    string Email,
    string? PhoneNumber,
    
    string FirstName,
    string LastName,
    string? MiddleName,
    
    Address Address,
    
    string Password
);