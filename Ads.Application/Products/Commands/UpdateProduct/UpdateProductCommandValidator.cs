using FluentValidation;

namespace Ads.Application.Products.Commands.UpdateProductCommand
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
       public UpdateProductCommandValidator() 
        { 
           // RuleFor(m => m.Name).NotEmpty().Unless(m=> !string.IsNullOrEmpty(m.Description));
        }
        
    }
}
