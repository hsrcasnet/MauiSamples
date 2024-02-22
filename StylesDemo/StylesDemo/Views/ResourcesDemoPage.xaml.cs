namespace StylesDemo.Views;

public partial class ResourcesDemoPage : ContentPage
{
    public ResourcesDemoPage()
    {
        this.InitializeComponent();

        var primaryColor = (Color)Application.Current.Resources["PrimaryColor"];
        this.PrimaryColorEntry.Text = primaryColor.ToArgbHex();
    }

    private void UpdatePrimaryColor(object sender, EventArgs e)
    {
        var newPrimaryColor = Color.FromArgb(this.PrimaryColorEntry.Text);
        Application.Current.Resources["PrimaryColor"] = newPrimaryColor;
    }
}