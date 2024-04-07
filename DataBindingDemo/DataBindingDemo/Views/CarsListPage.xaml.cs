using DataBindingDemo.Model;
using DataBindingDemo.ViewModels;

namespace DataBindingDemo.Views;

public partial class CarsListPage : ContentPage
{
    public CarsListPage()
    {
        this.InitializeComponent();
        this.BindingContext = new CarsViewModel();
    }

    private async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is Car car)
        {
            await this.DisplayAlert("ListView.ItemTapped", $"{car.Brand} {car.Model} was tapped.", "OK");

            // Deselect item
            ((ListView)sender).SelectedItem = null;
        }
    }
}