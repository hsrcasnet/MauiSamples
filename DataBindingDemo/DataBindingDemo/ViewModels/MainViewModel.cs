using System.Windows.Input;
using DataBindingDemo.Services;

namespace DataBindingDemo.ViewModels
{
    public class MainViewModel
    {
        private readonly INavigationService navigationService;
        private ICommand navigateToPageCommand;

        public MainViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public ICommand NavigateToPageCommand => this.navigateToPageCommand ??=
            new Command<string>(async (page) => await this.NavigateToPageAsync(page));

        private async Task NavigateToPageAsync(string pageName)
        {
            await this.navigationService.PushAsync(pageName);
        }
    }
}
