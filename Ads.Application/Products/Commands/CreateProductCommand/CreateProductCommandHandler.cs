using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using AutoMapper;
using MediatR;

namespace Ads.Application.Products.Commands.CreateProductCommand
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand , ProductEntity>
    {
        private readonly ICommonRepository<ProductEntity> _repository;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(ICommonRepository<ProductEntity> entity, IMapper mapper)
        {
            _repository = entity;
            _mapper = mapper;
        }

        public async Task<ProductEntity> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<ProductEntity>(request);
            var result = await _repository.InsertAsync(product, cancellationToken);
            return result;
        }
    }
}
