using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Ads.Application.Ads.Commands.CreateAdCommand
{
    public class CreateAdCommandHandler : IRequestHandler<CreateAdCommand, AdEntity>
    {
        private readonly ICommonRepository<AdEntity> _repository;
        private readonly IMapper _mapper;

        public CreateAdCommandHandler(ICommonRepository<AdEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<AdEntity> Handle(CreateAdCommand request, CancellationToken cancellationToken)
        {
            var ad = _mapper.Map<AdEntity>(request);
            var result = await _repository.InsertAsync(ad, cancellationToken);
            return result;
        }
    }
}
