using Ads.Application.Ads.Commands.CreateAdCommand;
using Ads.Application.Budgets.Commands.CreateBudgetCommand;
using Ads.Application.Campaigns.Commands.CreateCampaignCommand;
using Ads.Application.Categories.Commands.CreateCategoryCommand;
using Ads.Application.Credits.Commands.CreateCreditCommand;
using Ads.Application.Products.Commands.CreateProductCommand;
using Ads.Domain.Entities;
using AutoMapper;

namespace Ads.Application.Common.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            // Mapping pour Ad
            CreateMap<CreateAdCommand, AdEntity>().ReverseMap();

            // Mapping pour Budget
            CreateMap<CreateBudgetCommand, BudgetEntity>().ReverseMap();

            // Mapping pour Campaign
            CreateMap<CreateCampaignCommand, CampaignEntity>().ReverseMap();

            // Mapping pour Category
            CreateMap<CreateCategoryCommand, CategoryEntity>().ReverseMap();

            // Mapping pour Product
            CreateMap<CreateProductCommand, ProductEntity>().ReverseMap();

            // Mapping pour Promotion
            CreateMap<CreateCreditCommand, CreditEntity>().ReverseMap();

        }
    }
}
