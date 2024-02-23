using DataBindingDemo.ViewModels;

namespace DataBindingDemo.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        this.InitializeComponent();
        this.BindingContext = new LoginViewModel();
    }
}