using FluentValidation;

namespace Authentication.Application.Users.Commands.RegisterUserCommand;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator() 
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }

}
