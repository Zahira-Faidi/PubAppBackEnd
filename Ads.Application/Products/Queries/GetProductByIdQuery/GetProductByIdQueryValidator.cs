using FluentValidation;

namespace Ads.Application.Products.Queries.GetProductByIdQuery
{
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public  GetProductByIdQueryValidator()
        {
            RuleFor(x=> x.Id).NotEmpty();
        }
    }
}
