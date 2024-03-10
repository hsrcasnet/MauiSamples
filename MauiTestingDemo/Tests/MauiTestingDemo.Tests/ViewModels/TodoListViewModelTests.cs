using System;
using FluentAssertions;
using MauiTestingDemo.ViewModels;
using Moq.AutoMock;
using Xunit;

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
            var viewModel = this.autoMocker.CreateInstance<CalculatorViewModel>();

            // Act
            await viewModel.IncrementCounterCommand.ExecuteAsync(null);

            // Assert
            viewModel.Count.Should().Be(1);
        }

        [Fact]
        public async Task ShouldSumTwoValues()
        {
            // Arrange
            var viewModel = this.autoMocker.CreateInstance<CalculatorViewModel>();

            // Act
            await viewModel.IncrementCounterCommand.ExecuteAsync(null);
            await viewModel.IncrementCounterCommand.ExecuteAsync(null);
            await viewModel.IncrementCounterCommand.ExecuteAsync(null);

            // Assert
            viewModel.Count.Should().Be(3);
        }
    }
}