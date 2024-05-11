using FluentValidation;

namespace Authentication.Application.Users.Commands.DeleteUserCommand;

internal class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }

}