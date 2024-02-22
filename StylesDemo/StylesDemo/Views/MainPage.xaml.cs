namespace StylesDemo.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NavigateToResourcesDemoPage(object sender, EventArgs e)
        {
            _ = this.Navigation.PushAsync(new ResourcesDemoPage());
        }

        private void NavigateToExplicitStylesDemoPage(object sender, EventArgs e)
        {
            _ = this.Navigation.PushAsync(new ExplicitStylesDemoPage());
        }

        private void NavigateToImplicitStylesDemoPage(object sender, EventArgs e)
        {
            _ = this.Navigation.PushAsync(new ImplicitStylesDemoPage());
        }
    }
}
