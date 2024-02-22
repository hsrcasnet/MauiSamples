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
    }
}
