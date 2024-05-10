using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
