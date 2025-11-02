namespace TourApp.Application.Features.Users.Commands.ConfirmUserRegister;

public record ConfirmUserRegisterCommand(
    CodeConfirmation Confirmation
) : IRequest<Result>;