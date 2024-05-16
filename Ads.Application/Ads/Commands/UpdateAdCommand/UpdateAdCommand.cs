﻿using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Ads.Commands.UpdateAdCommand
{
    public record UpdateAdCommand
        (
            string Id,
            string Name,
            DateTimeOffset StartDate,
            DateTimeOffset EndDate,
            string CampaignId,
            string CreditId
        ) : IRequest<AdEntity>;
}
