using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Ads.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Ads.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastractureConfiguration(this IServiceCollection services)
        {
            services.AddTransient<ICommonRepository<ProductEntity>, ProductRepository>();
            services.AddTransient<ICommonRepository<AdEntity>, AdRepository>();
            services.AddTransient<ICommonRepository<BudgetEntity>, BudgetRepository>();
            services.AddTransient<ICommonRepository<CampaignEntity>, CampaignRepository>();
            services.AddTransient<ICommonRepository<CategoryEntity>, CategoryRepository>();
            services.AddTransient<ICommonRepository<PromotionEntity>, PromotionRepository>();

            return services;
        }
    }
}
