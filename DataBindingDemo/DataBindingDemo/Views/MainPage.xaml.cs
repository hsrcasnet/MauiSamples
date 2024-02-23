using DataBindingDemo.Services;
using DataBindingDemo.ViewModels;

namespace DataBindingDemo.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        this.InitializeComponent();

        // Demo: Manually create instance of MainViewModel and all its dependencies.
        //       Is there a better way to connect a Page with its ViewModel?
        this.BindingContext = new MainViewModel(new NavigationService(this));
    }
}