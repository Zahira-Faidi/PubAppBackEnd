using Ads.Application.Budgets.Queries.GetBudgets;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ads.Application.UnitTests.Budgets.Queries.GetBudgets
{
    public class GetBudgetsQueryHandlerTest
    {
        private readonly Mock<IBudgetRepository> _mockRepository;
        private readonly GetBudgetsQueryHandler _handler;

        public GetBudgetsQueryHandlerTest()
        {
            _mockRepository = new Mock<IBudgetRepository>();
            _handler = new GetBudgetsQueryHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ReturnsAllBudgets()
        {
            // Arrange
            var budgets = new List<BudgetEntity>
            {
                new BudgetEntity { Id = "1", Name = "Marketing", TotalBudget = 1000 },
                new BudgetEntity { Id = "2", Name = "Development", TotalBudget = 2000 }
            };
            var query = new GetBudgetsQuery();

            _mockRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                           .ReturnsAsync(budgets);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, b => b.Id == "1" && b.Name == "Marketing");
            Assert.Contains(result, b => b.Id == "2" && b.Name == "Development");
        }
    }
}
