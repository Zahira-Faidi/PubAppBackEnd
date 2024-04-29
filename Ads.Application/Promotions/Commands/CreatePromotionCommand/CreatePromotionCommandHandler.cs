using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Ads.Application.Promotions.Commands.CreatePromotionCommand
{
    public class CreatePromotionCommandHandler : IRequestHandler<CreatePromotionCommand, PromotionEntity>
    {
        private readonly ICommonRepository<PromotionEntity> _repository;
        private readonly IMapper _mapper;

        public CreatePromotionCommandHandler(ICommonRepository<PromotionEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PromotionEntity> Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
        {
            var promotion = _mapper.Map<PromotionEntity>(request);
            var result = await _repository.InsertAsync(promotion, cancellationToken);
            return result;
        }
    }
}
