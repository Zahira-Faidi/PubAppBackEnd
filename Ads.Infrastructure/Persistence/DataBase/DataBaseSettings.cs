namespace Ads.Infrastructure.Persistence.DataBase
{
    public class DataBaseSettings
    {
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
        public string? CategoriesCollectionName { get; set; }
        public string? ProductsCollectionName { get; set; }
        public string? CampaignsCollectionName { get; set; }
        public string? AdsCollectionName { get; set; }
        public string? BudgetsCollectionName { get; set; }
        public string? PromotionsCollectionName { get; set; }

    }
}
