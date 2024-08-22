namespace NavigationDemo.Views
{
    public partial class ModalPage : ContentPage
    {
        public ModalPage()
        {
            this.InitializeComponent();
        }

        private void NavigateBackModalAsync(object sender, EventArgs e)
        {
            this.Navigation.PopModalAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}