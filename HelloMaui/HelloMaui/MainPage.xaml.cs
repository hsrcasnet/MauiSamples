namespace HelloMaui
{
    public partial class MainPage : ContentPage
    {
        private int count = 0;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            this.count++;
            this.MyButton.Text = $"Clicked {this.count}";
        }
    }
}
