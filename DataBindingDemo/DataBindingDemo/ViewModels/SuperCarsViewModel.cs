using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DataBindingDemo.Model;

namespace DataBindingDemo.ViewModels
{
    public class SuperCarsViewModel : INotifyPropertyChanged
    {
        public SuperCarsViewModel()
        {
            this.AddCarCommand = new Command(() => this.AddCar());
            this.UpdateCarCommand = new Command(() => this.UpdateCar());

            var cars = new List<Car>
            {
                new Car
                {
                    Brand = "Lamborghini",
                    Model = "Aventador SVJ"
                },
                new Car
                {
                    Brand = "McLaren",
                    Model = "720S"
                }
            };

            var carItemViewModels = cars.Select(c => new CarItemViewModel(c));

            this.Cars = new ObservableCollection<CarItemViewModel>(carItemViewModels);
        }

        public string Title { get; } = "Super Cars";

        public ObservableCollection<CarItemViewModel> Cars { get; }

        public ICommand AddCarCommand { get; }

        public ICommand UpdateCarCommand { get; }

        private void AddCar()
        {
            var car = new Car
            {
                Brand = "Porsche",
                Model = "911",
            };
            this.Cars.Add(new CarItemViewModel(car));
        }

        private void UpdateCar()
        {
            this.Cars[0].Brand = "Ferrari";
            this.Cars[0].Model = "488 Pista";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}