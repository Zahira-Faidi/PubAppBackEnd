using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Ads.Application.Credits.Commands.CreateCreditCommand;

public class CreateCreditCommandHandler : IRequestHandler<CreateCreditCommand, CreditEntity>
{
    private readonly ICreditRepository _repository;
    private readonly IMapper _mapper;
    public CreateCreditCommandHandler(ICreditRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CreditEntity> Handle(CreateCreditCommand request, CancellationToken cancellationToken)
    {
        var credit = _mapper.Map<CreditEntity>(request);
        var result = await _repository.InsertAsync(credit, cancellationToken);
        return result;
    }
}
