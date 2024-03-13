using LocalizationDemo.ViewModels;

namespace LocalizationDemo.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel mainViewModel)
        {
            this.InitializeComponent();
            this.BindingContext = mainViewModel;
        }
    }
}
