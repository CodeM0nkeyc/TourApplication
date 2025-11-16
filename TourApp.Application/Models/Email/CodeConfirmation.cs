namespace TourApp.Application.Models.Email;

public record CodeConfirmation(
    string Email,
    int Code
);