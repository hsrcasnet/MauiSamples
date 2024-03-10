using FluentAssertions;
using MauiTestingDemo.Services;
using MauiTestingDemo.ViewModels;
using Moq;
using Moq.AutoMock;

namespace MauiTestingDemo.Tests.ViewModels
{
    public class TodoListViewModelTests
    {
        private readonly AutoMocker autoMocker;

        public TodoListViewModelTests()
        {
            this.autoMocker = new AutoMocker();
        }

        [Fact]
        public async Task ShouldIncrementCounter()
        {
            // Arrange
            var calculatorServiceMock = this.autoMocker.GetMock<ICalculatorService>();
            calculatorServiceMock.Setup(c => c.Increment(It.IsAny<int>()))
                .Returns(1);

            var viewModel = this.autoMocker.CreateInstance<CalculatorViewModel>();

            // Act
            await viewModel.IncrementCounterCommand.ExecuteAsync(null);

            // Assert
            viewModel.Count.Should().Be(1);

            calculatorServiceMock.Verify(c => c.Increment(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void ShouldSumTwoValues()
        {
            // Arrange
            var calculatorServiceMock = this.autoMocker.GetMock<ICalculatorService>();
            calculatorServiceMock.Setup(c => c.Sum(It.IsAny<decimal>(), It.IsAny<decimal>()))
                .Returns(99.99m);

            var viewModel = this.autoMocker.CreateInstance<CalculatorViewModel>();

            viewModel.Summand1 = 1m;
            viewModel.Summand2 = 2m;

            // Act
            viewModel.CalculateSumCommand.Execute(null);

            // Assert
            viewModel.SumResult.Should().Be("99.99");

            calculatorServiceMock.Verify(c => c.Sum(1m, 2m), Times.Once);
        }
    }
}