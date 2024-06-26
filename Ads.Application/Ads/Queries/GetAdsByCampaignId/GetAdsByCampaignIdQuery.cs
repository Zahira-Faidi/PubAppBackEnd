﻿using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Ads.Queries.GetAdsByCampaignId;

public class GetAdsByCampaignIdQuery : IRequest<List<AdEntity>>
{
    public string CampaignId { get; set; }
    public bool IsDeleted { get; set; }

    public GetAdsByCampaignIdQuery(string campaignId)
    {
        CampaignId = campaignId;
    }
}
