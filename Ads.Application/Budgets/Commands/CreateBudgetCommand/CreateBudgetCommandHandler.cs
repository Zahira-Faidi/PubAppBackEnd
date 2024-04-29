using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Ads.Application.Budgets.Commands.CreateBudgetCommand
{
    public class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, BudgetEntity>
    {
        private readonly ICommonRepository<BudgetEntity> _repository;

        private readonly IMapper _mapper;

        public CreateBudgetCommandHandler(ICommonRepository<BudgetEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BudgetEntity> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
        {
            var budget = _mapper.Map<BudgetEntity>(request);
            var result = await _repository.InsertAsync(budget, cancellationToken);
            return result;
        }
    }
}
