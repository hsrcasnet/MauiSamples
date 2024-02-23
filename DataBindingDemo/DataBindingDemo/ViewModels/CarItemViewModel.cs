using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataBindingDemo.Model;

namespace DataBindingDemo.ViewModels
{
    public class CarItemViewModel : INotifyPropertyChanged
    {
        private string brand;

        public CarItemViewModel(Car car)
        {
            this.Brand = car.Brand;
            this.Model = $"Model: {car.Model}";
        }

        // Demo: Property Brand supports property changes
        //       If we update the Brand property,
        //       all related bindings in the UI get updated.
        public string Brand
        {
            get => this.brand;
            set
            {
                this.brand = value;
                this.OnPropertyChanged(nameof(this.Brand));
            }
        }

        // Demo: Property Model does not support property changes
        //       So, if we update Model with a new value, the UI 
        //       does not reflect this change.
        public string Model { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}