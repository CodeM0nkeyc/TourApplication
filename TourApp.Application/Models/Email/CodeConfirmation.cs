namespace TourApp.Application.Models;

public record CodeConfirmation(
    string Email,
    int Code
);