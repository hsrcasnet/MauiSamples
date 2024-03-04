using PlatformDivergenceApp.ViewModels;

namespace PlatformDivergenceApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel mainViewModel)
        {
            this.InitializeComponent();
            this.BindingContext = mainViewModel;
        }

        private async void NavigateToSettingsPageAsync(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new SettingsPage());
        }
    }

}
