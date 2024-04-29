namespace Ads.Domain.DTOs
{
    public record CategoryGetDto
    (
        string Id,
        string Name
    );

    public record CategoryCreateDto
    (
        string Name
    );

    public record CategoryUpdateDto
    (
        string Id,
        string Name
    );
}
