using Ads.Application.Ads.Commands.CreateAdCommand;
using Ads.Application.Budgets.Commands.CreateBudgetCommand;
using Ads.Application.Campaigns.Commands.CreateCampaignCommand;
using Ads.Application.Categories.Commands.CreateCategoryCommand;
using Ads.Application.Products.Commands.CreateProductCommand;
using Ads.Application.Promotions.Commands.CreatePromotionCommand;
using Ads.Domain.Entities;
using AutoMapper;

namespace Ads.Application.Common.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            // Mapping pour Ad
            //CreateMap<AdEntity, AdGetDto>().ReverseMap();
            //CreateMap<AdCreateDto, AdEntity>().ReverseMap();
            //CreateMap<AdUpdateDto, AdEntity>().ReverseMap();
            CreateMap<CreateAdCommand, AdEntity>().ReverseMap();

            // Mapping pour Budget
            //CreateMap<BudgetEntity, BudgetGetDto>().ReverseMap();
            //CreateMap<BudgetCreateDto, BudgetEntity>().ReverseMap();
            //CreateMap<BudgetUpdateDto, BudgetEntity>().ReverseMap();
            CreateMap<CreateBudgetCommand, BudgetEntity>().ReverseMap();

            // Mapping pour Campaign
            //CreateMap<CampaignEntity, CampaignGetDto>().ReverseMap();
            //CreateMap<CampaignCreateDto, CampaignEntity>().ReverseMap();
            //CreateMap<CampaignUpdateDto, CampaignEntity>().ReverseMap();
            CreateMap<CreateCampaignCommand, CampaignEntity>().ReverseMap();

            // Mapping pour Category
            //CreateMap<CategoryEntity, CategoryGetDto>().ReverseMap();
            //CreateMap<CategoryCreateDto, CategoryEntity>().ReverseMap();
            //CreateMap<CategoryUpdateDto, CategoryEntity>().ReverseMap();
            CreateMap<CreateCategoryCommand, CategoryEntity>().ReverseMap();

            // Mapping pour Product
            //CreateMap<ProductEntity, ProductGetDto>().ReverseMap();
            //CreateMap<ProductCreateDto, ProductEntity>().ReverseMap();
            //CreateMap<ProductUpdateDto, ProductEntity>().ReverseMap();
            CreateMap<CreateProductCommand, ProductEntity>().ReverseMap();

            // Mapping pour Promotion
            //CreateMap<PromotionEntity, PromotionGetDto>().ReverseMap();
            //CreateMap<PromotionCreateDto, PromotionEntity>().ReverseMap();
            //CreateMap<PromotionUpdateDto, PromotionEntity>().ReverseMap();
            CreateMap<CreatePromotionCommand, PromotionEntity>().ReverseMap();

        }
    }
}
