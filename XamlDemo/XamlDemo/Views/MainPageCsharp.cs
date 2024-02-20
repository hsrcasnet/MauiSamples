namespace XamlDemo.Views
{
    public class MainPageCsharp : ContentPage
    {
        public MainPageCsharp()
        {
            var label1 = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Text = "Welcome to .NET MAUI!"
            };

            var button1 = new Microsoft.Maui.Controls.Button
            {
                Text = "OK",
            };
            button1.Clicked += this.Button_Clicked;

            this.Content = new VerticalStackLayout
            {
                Children =
                {
                   label1,
                   button1
                }
            };
        }

        private void Button_Clicked(object? sender, EventArgs e)
        {
            //...
        }
    }
}