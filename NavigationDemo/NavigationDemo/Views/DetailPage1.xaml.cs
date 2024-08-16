using System.Diagnostics;

namespace NavigationDemo.Views
{
    public partial class DetailPage1 : ContentPage
    {
        public DetailPage1()
        {
            this.InitializeComponent();
        }

        private async void NavigateToDetailPage2Async(object sender, EventArgs e)
        {
            await this.Navigation.PushAsync(new DetailPage2());
        }

        protected override void OnAppearing()
        {
            Debug.WriteLine("DetailPage1: OnAppearing");
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            Debug.WriteLine("DetailPage1: OnDisappearing");
            base.OnDisappearing();
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            Debug.WriteLine("DetailPage1: OnNavigatedTo");
            base.OnNavigatedTo(args);
        }

        protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
        {
            Debug.WriteLine("DetailPage1: OnNavigatingFrom");
            base.OnNavigatingFrom(args);
        }

        protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
        {
            Debug.WriteLine("DetailPage1: OnNavigatedFrom");
            base.OnNavigatedFrom(args);
        }
    }
}