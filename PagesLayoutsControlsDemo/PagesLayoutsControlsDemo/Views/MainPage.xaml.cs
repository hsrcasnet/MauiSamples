namespace PagesLayoutsControlsDemo.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NavigateToGridDemoPage(object sender, EventArgs e)
        {
            _ = this.Navigation.PushAsync(new GridDemoPage());
        }

        private void NavigateToStackLayoutDemoPage(object sender, EventArgs e)
        {
            _ = this.Navigation.PushAsync(new StackLayoutDemoPage());
        }

        private void NavigateToControlsDemoPage(object sender, EventArgs e)
        {
            _ = this.Navigation.PushAsync(new ControlsDemoPage());
        }
    }
}
