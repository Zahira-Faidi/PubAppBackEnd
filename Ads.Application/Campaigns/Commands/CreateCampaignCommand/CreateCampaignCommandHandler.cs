using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Ads.Application.Campaigns.Commands.CreateCampaignCommand
{
    public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, CampaignEntity>
    {
        private readonly ICampaignRepository _repository;
        private readonly IMapper _mapper;
        public CreateCampaignCommandHandler(ICampaignRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CampaignEntity> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaign = _mapper.Map<CampaignEntity>(request);
            var result = await _repository.InsertAsync(campaign, cancellationToken);
            return result;
        }
    }
}
