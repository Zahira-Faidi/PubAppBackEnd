namespace Ads.Domain.DTOs
{
    public class PromotionDTO
    {
        public record PromotionGetDto
        (
            string Id,
            string Description,
            double Discount,
            List<string> Products
        );

        public record PromotionCreateDto
        (
            string Description,
            double Discount,
            List<string> Products
        );

        public record PromotionUpdateDto
        (
            string Id,
            string Description,
            double Discount,
            List<string> Products
        );
    }
}
