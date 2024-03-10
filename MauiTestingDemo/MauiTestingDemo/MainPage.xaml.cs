namespace MauiTestingDemo
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            this.count++;

            if (this.count == 1)
                this.CounterBtn.Text = $"Clicked {this.count} time";
            else
                this.CounterBtn.Text = $"Clicked {this.count} times";

            SemanticScreenReader.Announce(this.CounterBtn.Text);
        }
    }

}
