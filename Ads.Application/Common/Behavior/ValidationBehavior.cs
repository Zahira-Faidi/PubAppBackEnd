using ErrorOr;
using FluentValidation;
using MediatR;

namespace Ads.Application.Common.Behavior
{
    public class ValidateRegisterCommandBehavior<TRequest, TResponse>
     : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
     where TResponse : IErrorOr
    {
        private readonly IValidator<TRequest>? _validator = null;

        public ValidateRegisterCommandBehavior(IValidator<TRequest>? validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validator is null) return await next();
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid) return await next();
            //Before the handler

            //After the handler
            var errors = validationResult.Errors
            .ConvertAll(validationFailure => Error.Validation(
                    validationFailure.PropertyName,
                    validationFailure.ErrorMessage
            )
            ).ToList();

            return (dynamic)errors;

        }
    }
}