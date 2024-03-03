using ForexApp.Services;
using ForexApp.ViewModels;

namespace ForexApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            //var forexService = new ForexServiceMock();
            var forexService = new ForexService(new ForexServiceConfiguration());
            this.BindingContext = new MainViewModel(forexService);
        }
    }
}