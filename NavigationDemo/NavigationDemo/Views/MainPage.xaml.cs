using System.Diagnostics;

namespace NavigationDemo.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void NavigateToDetailPage1Async(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new DetailPage1(), animated: true);
        }

        private async void NavigateToModalPageAsync(object sender, EventArgs e)
        {
            await this.Navigation.PushModalAsync(new NavigationPage(new ModalPage()), animated: true);
        }

        protected override void OnAppearing()
        {
            Debug.WriteLine("MainPage: OnAppearing");
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            Debug.WriteLine("MainPage: OnDisappearing");
            base.OnDisappearing();
        }
    }
}