using DataBindingDemo.ViewModels;

namespace DataBindingDemo.Views;

public partial class SuperCarsListPage : ContentPage
{
    public SuperCarsListPage()
    {
        this.InitializeComponent();
        this.BindingContext = new SuperCarsViewModel();
    }
}