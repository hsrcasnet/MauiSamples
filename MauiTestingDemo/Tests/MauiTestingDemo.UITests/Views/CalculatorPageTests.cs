using FluentAssertions;
using Platform = Xamarin.UITest.Platform;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace MauiTestingDemo.UITests
{
    public class CalculatorPageTests
    {
        [Theory]
        [InlineData(Platform.Android)]
        //[InlineData(Platform.iOS)]
        public void ShouldTapIncrementButtonOnce(Platform platform)
        {
            // Arrange
            var app = AppInitializer.StartApp(platform);

            Query countButton = x => x.Marked("IncrementButton");
            app.WaitForElement(countButton);

            // Act
            app.Tap(countButton);

            // Assert
            var action = () => app.WaitForElement(x => x.Marked("Clicked 1 time"), "Button was not clicked");
            action.Should().NotThrow();

            app.Screenshot("ShouldTapIncrementButtonOnce");
        }

        [Theory]
        [InlineData(Platform.Android)]
        //[InlineData(Platform.iOS)]
        public void ShouldTapIncrementButtonTwice(Platform platform)
        {
            // Arrange
            var app = AppInitializer.StartApp(platform);

            Query countButton = x => x.Marked("IncrementButton");
            app.WaitForElement(countButton);

            // Act
            app.Tap(countButton);
            app.Tap(countButton);

            // Assert
            var action = () => app.WaitForElement(x => x.Marked("Clicked 2 times"), "Button was not clicked");
            action.Should().NotThrow();

            app.Screenshot("ShouldTapIncrementButtonTwice");
        }

        [Theory]
        [InlineData(Platform.Android)]
        //[InlineData(Platform.iOS)]
        public void ShouldCalculateSumOfTwoValues(Platform platform)
        {
            // Arrange
            var app = AppInitializer.StartApp(platform);

            app.EnterText("Summand1Entry", "1");
            app.EnterText("Summand2Entry", "2");

            // Act
            app.Tap("SumButton");

            // Assert
            var sumResultEntry = app.WaitForElement(x => x.Marked("SumResult"))[0];
            sumResultEntry.Text.Should().Be("3.00");

            app.Screenshot("ShouldCalculateSumOfTwoValues");
        }
    }
}
