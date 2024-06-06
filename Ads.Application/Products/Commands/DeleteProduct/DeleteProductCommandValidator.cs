using FluentValidation;

namespace Ads.Application.Products.Commands.DeleteProductCommand
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator() 
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("Id is required");
        }
    }
}
