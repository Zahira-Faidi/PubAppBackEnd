using Ads.Application.Common.Interfaces;
using Ads.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Ads.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastractureConfiguration(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IBudgetRepository, BudgetRepository>();
            services.AddTransient<ICampaignRepository, CampaignRepository> ();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IPromotionRepository, PromotionRepository>();
            services.AddTransient<IAdRepository, AdRepository>(); 

            return services;
        }
    }
}
