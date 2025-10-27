namespace TourApp.Application.Features.Users.Commands.ConfirmUserRegister;

public record ConfirmUserRegisterCommand(
    string Email,
    int ConfirmationCode
) : IRequest<ConfirmationResult>;