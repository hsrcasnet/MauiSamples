using DataBindingDemo.Model;

namespace DataBindingDemo.Views;

public partial class CarsListPage : ContentPage
{
    public CarsListPage()
    {
        this.InitializeComponent();
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