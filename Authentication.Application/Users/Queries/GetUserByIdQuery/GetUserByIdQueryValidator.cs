using FluentValidation;

namespace Authentication.Application.Users.Queries.GetUserByIdQuery;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{

    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}