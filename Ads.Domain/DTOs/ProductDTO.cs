namespace Ads.Domain.DTOs
{
        public record ProductGetDto(
            string Id,
            string Name,
            string Description,
            string Image,
            double Price,
            int Quantity,
            string CategoryId,
            List<string> Promotions
        );

        public record ProductCreateDto(
            string Name,
            string Description,
            string Image,
            double Price,
            int Quantity,
            string CategoryId,
            List<string> Promotions
        );

        public record ProductUpdateDto(
            string Id,
            string Name,
            string Description,
            string Image,
            double Price,
            int Quantity,
            string CategoryId,
            List<string> Promotions
        );
}
