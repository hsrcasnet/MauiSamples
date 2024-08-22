using System.Diagnostics;

namespace NavigationDemo.Views
{
    public partial class DetailPage2 : ContentPage
    {
        public DetailPage2()
        {
            this.InitializeComponent();
        }

        private async void NavigateBackAsync(object sender, EventArgs e)
        {
            await this.Navigation.PopAsync();
        }

        protected override void OnAppearing()
        {
            Debug.WriteLine("DetailPage2: OnAppearing");
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            Debug.WriteLine("DetailPage2: OnDisappearing");
            base.OnDisappearing();
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            Debug.WriteLine("DetailPage2: OnNavigatedTo");
            base.OnNavigatedTo(args);
        }

        protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
        {
            Debug.WriteLine("DetailPage2: OnNavigatingFrom");
            base.OnNavigatingFrom(args);
        }

        protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
        {
            Debug.WriteLine("DetailPage2: OnNavigatedFrom");
            base.OnNavigatedFrom(args);
        }
    }
}