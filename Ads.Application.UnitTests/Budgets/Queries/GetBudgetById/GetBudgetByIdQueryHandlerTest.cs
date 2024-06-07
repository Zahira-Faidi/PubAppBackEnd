using Ads.Application.Budgets.Queries.GetBudgetById;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ads.Application.UnitTests.Budgets.Queries.GetBudgetById
{
    public class GetBudgetByIdQueryHandlerTest
    {
        private readonly Mock<IBudgetRepository> _mockRepository;
        private readonly GetBudgetByIdQueryHandler _handler;

        public GetBudgetByIdQueryHandlerTest()
        {
            _mockRepository = new Mock<IBudgetRepository>();
            _handler = new GetBudgetByIdQueryHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ExistingId_ReturnsBudget()
        {
            // Arrange
            var budgetId = "1";
            var expectedBudget = new BudgetEntity { Id = budgetId, Name = "Marketing", TotalBudget = 1000 };
            var query = new GetBudgetByIdQuery(budgetId);

            _mockRepository.Setup(r => r.GetDetailsAsync(budgetId, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(expectedBudget);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedBudget, result);
        }

        [Fact]
        public async Task Handle_NonExistingId_ReturnsNull()
        {
            // Arrange
            var budgetId = "2";
            var query = new GetBudgetByIdQuery(budgetId);

            _mockRepository.Setup(r => r.GetDetailsAsync(budgetId, It.IsAny<CancellationToken>()))
                           .ReturnsAsync((BudgetEntity)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
