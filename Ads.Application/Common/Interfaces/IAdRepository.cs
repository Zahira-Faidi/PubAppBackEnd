﻿using Ads.Application.Common.Base;
using Ads.Domain.Entities;

namespace Ads.Application.Common.Interfaces
{
    public interface IAdRepository : IBaseRepository<AdEntity>
    {
        Task<List<AdEntity>> GetAllAdsByCampaignId(string campaingId, CancellationToken cancellationToken);
    }
}
