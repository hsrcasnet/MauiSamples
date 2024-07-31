namespace PrismMauiApp.ViewModels
{
    public class AboutViewModel
    {
        private readonly IDateTime dateTime;
        private readonly INavigationService navigationService;

        private IAsyncCommand goBackCommand;

        public AboutViewModel(
            IDateTime dateTime,
            INavigationService navigationService)
        {
            this.dateTime = dateTime;
            this.navigationService = navigationService;
        }

        public string Copyright
        {
            get => $"© {this.dateTime.Now.Year}";
        }

        public IAsyncCommand GoBackCommand
        {
            get => this.goBackCommand ??= new AsyncDelegateCommand(this.GoBackAsync);
        }

        private async Task GoBackAsync()
        {
            var navigationParameters = new NavigationParameters
            {
                { KnownNavigationParameters.UseModalNavigation, true }
            };
            var result = await this.navigationService.GoBackAsync(navigationParameters);
        }
    }
}
