﻿using Ads.Application.Common.Exceptions;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Ads.Commands.UpdateAd
{
    public class UpdateAdCommandHandler : IRequestHandler<UpdateAdCommand, AdEntity>
    {
        private readonly IAdRepository _repository;
        public UpdateAdCommandHandler(IAdRepository repository)
        {
            _repository = repository;
        }

        public async Task<AdEntity> Handle(UpdateAdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingAd = await _repository.GetDetailsAsync(request.Id, cancellationToken);
                if (existingAd == null)
                {
                    throw new AdNotFoundException($"Ad with id {request.Id} not found");
                }

                existingAd.Name = request.Name ?? existingAd.Name;
                existingAd.CampaignId = request.CampaignId ?? existingAd.CampaignId;
                existingAd.StartDate = request.StartDate;
                existingAd.EndDate = request.EndDate;
                existingAd.Credit = request.Credit !=0 ? request.Credit : existingAd.Credit;
                existingAd.Consumed = request.Consumed != 0 ? request.Consumed : existingAd.Consumed;
                existingAd.Impressions = request.Impressions != 0 ? request.Impressions : existingAd.Impressions;
                await _repository.UpdateAsync(request.Id, existingAd, cancellationToken);

                return existingAd;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update Ad: {ex.Message}", ex);
            }
        }
    }
}
