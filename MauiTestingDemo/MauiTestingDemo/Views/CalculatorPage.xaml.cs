using MauiTestingDemo.ViewModels;

namespace MauiTestingDemo
{
    public partial class CalculatorPage : ContentPage
    {
        public CalculatorPage(CalculatorViewModel calculatorViewModel)
        {
            this.InitializeComponent();
            this.BindingContext = calculatorViewModel;
        }
    }

}
