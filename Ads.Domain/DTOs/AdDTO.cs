namespace Ads.Domain.DTOs
{
    public class AdDTO
    {
        public record AdGetDto
        (
            string Id,
            string Content,
            double AllocatedBudget,
            string CampaignId
        );

        public record AdCreateDto
        (
            string Content,
            double AllocatedBudget,
            string CampaignId
        );

        public record AdUpdateDto
        (
            string Id,
            string Content,
            double AllocatedBudget,
            string CampaignId
        );
    }
}
