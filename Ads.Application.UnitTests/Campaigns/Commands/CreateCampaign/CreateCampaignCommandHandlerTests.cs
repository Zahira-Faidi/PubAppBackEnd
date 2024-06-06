using Ads.Application.Campaigns.Commands.CreateCampaign;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Ads.Domain.Enums;
using AutoMapper;
using Moq;

namespace Ads.Application.UnitTests.Campaigns.Commands.CreateCampaign
{
    public class CreateCampaignCommandHandlerTests
    {
        private readonly Mock<ICampaignRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateCampaignCommandHandler _handler;

        public CreateCampaignCommandHandlerTests()
        {
            _mockRepository = new Mock<ICampaignRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateCampaignCommandHandler(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldCallRepositoryInsertAsync()
        {
            // Arrange
            var command = new CreateCampaignCommand(
                Name: "Test Campaign",
                StartDate: DateTimeOffset.UtcNow,
                EndDate: DateTimeOffset.UtcNow.AddDays(10),
                Impressions: 0,
                SellerId: "664da822850cb5483c37a403",
                BudgetId: "6644d886911727f9e9686acd"
            );

            var campaignEntity = new CampaignEntity
            {
                Name = command.Name,
                StartDate = command.StartDate,
                EndDate = command.EndDate,
                Impressions = command.Impressions,
                SellerId = command.SellerId,
                BudgetId = command.BudgetId,
                Status = Status.InDraft
            };

            _mockMapper.Setup(m => m.Map<CampaignEntity>(It.IsAny<CreateCampaignCommand>()))
                       .Returns(campaignEntity);

            _mockRepository.Setup(r => r.InsertAsync(It.IsAny<CampaignEntity>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(campaignEntity);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockMapper.Verify(m => m.Map<CampaignEntity>(command), Times.Once);
            _mockRepository.Verify(r => r.InsertAsync(It.Is<CampaignEntity>(c =>
                c.Name == command.Name &&
                c.StartDate == command.StartDate &&
                c.EndDate == command.EndDate &&
                c.Impressions == command.Impressions &&
                c.SellerId == command.SellerId &&
                c.BudgetId == command.BudgetId &&
                c.Status == Status.InDraft), It.IsAny<CancellationToken>()), Times.Once);

            Assert.Equal(Status.InDraft, result.Status);
        }

        [Fact]
        public async Task Handle_ShouldSetStatusToInDraft()
        {
            // Arrange
            var command = new CreateCampaignCommand(
                Name: "Test Campaign",
                StartDate: DateTimeOffset.UtcNow,
                EndDate: DateTimeOffset.UtcNow.AddDays(10),
                Impressions: 0,
                SellerId: "664da822850cb5483c37a403",
                BudgetId: "6644d886911727f9e9686acd"
            );

            var campaignEntity = new CampaignEntity
            {
                Name = command.Name,
                StartDate = command.StartDate,
                EndDate = command.EndDate,
                Impressions = command.Impressions,
                SellerId = command.SellerId,
                BudgetId = command.BudgetId,
                Status = Status.InDraft
            };

            _mockMapper.Setup(m => m.Map<CampaignEntity>(It.IsAny<CreateCampaignCommand>()))
                       .Returns(campaignEntity);

            _mockRepository.Setup(r => r.InsertAsync(It.IsAny<CampaignEntity>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(campaignEntity);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Status.InDraft, result.Status);
        }
    }
}
