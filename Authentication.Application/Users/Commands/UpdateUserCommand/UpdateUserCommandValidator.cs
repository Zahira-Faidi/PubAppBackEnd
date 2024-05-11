using FluentValidation;

namespace Authentication.Application.Users.Commands.UpdateUserCommand
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(m => m.FirstName).NotEmpty().Unless(m => !string.IsNullOrEmpty(m.LastName));
            RuleFor(m => m.LastName).NotEmpty().Unless(m => !string.IsNullOrEmpty(m.FirstName));
        }
    }
}
