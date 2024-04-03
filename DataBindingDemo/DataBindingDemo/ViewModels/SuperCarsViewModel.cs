using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DataBindingDemo.Model;

namespace DataBindingDemo.ViewModels
{
    public class SuperCarsViewModel : INotifyPropertyChanged
    {
        private bool isBusy;
        private CarItemViewModel selectedCar;
        private string selectedBrand;
        private string selectedModel;

        public SuperCarsViewModel()
        {
            this.AddCarCommand = new Command(
                execute: () => this.AddCar(),
                canExecute: () => !this.IsBusy);

            this.UpdateCarCommand = new Command(
                execute: () => _ = this.UpdateCarAsync(),
                canExecute: () => !this.IsBusy);

            var cars = new List<Car>
            {
                new Car
                {
                    Brand = "Lamborghini",
                    Model = "Revuelto"
                },
                new Car
                {
                    Brand = "McLaren",
                    Model = "720S"
                },
                new Car
                {
                    Brand = "Ferrari",
                    Model = "488 Pista Piloti"
                },
            };

            var carItemViewModels = cars.Select(c => new CarItemViewModel(c));

            this.Cars = new ObservableCollection<CarItemViewModel>(carItemViewModels);
        }

        public string Title => $"Super Cars {(this.IsBusy ? "(Loading...)" : "")}";

        public ObservableCollection<CarItemViewModel> Cars { get; }

        // Demo: SelectedCar binds to the SelectedItem property of the ListView
        public CarItemViewModel SelectedCar
        {
            get => this.selectedCar;
            set
            {
                if (this.SetProperty(ref this.selectedCar, value))
                {
                    this.SelectedBrand = value.Brand;
                    this.SelectedModel = value.Model;
                }
            }
        }

        public string SelectedBrand
        {
            get => this.selectedBrand;
            set => this.SetProperty(ref this.selectedBrand, value);
        }

        public string SelectedModel
        {
            get => this.selectedModel;
            set => this.SetProperty(ref this.selectedModel, value);
        }

        // Demo: Command binding with synchronous command handler

        public ICommand AddCarCommand { get; }

        private void AddCar()
        {
            this.IsBusy = true;

            try
            {
                // TODO: Perform input validation here

                var carItemViewModel = new CarItemViewModel
                {
                    Brand = this.SelectedBrand,
                    Model = this.SelectedModel,
                };

                this.Cars.Add(carItemViewModel);
            }
            catch (Exception)
            {
                // TODO: Inform user about error
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        // Demo: Command binding with asynchronous command handler

        public ICommand UpdateCarCommand { get; }

        private async Task UpdateCarAsync()
        {
            this.IsBusy = true;

            try
            {
                // TODO: Perform input validation here

                await Task.Delay(2000); // Simulate a long running task...

                if (this.SelectedCar is CarItemViewModel carItemViewModel)
                {
                    carItemViewModel.Brand = this.SelectedBrand;
                    carItemViewModel.Model = this.SelectedModel;
                }
            }
            catch (Exception)
            {
                // TODO: Inform user about error
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        public bool IsBusy
        {
            get => this.isBusy;
            private set
            {
                if (this.SetProperty(ref this.isBusy, value))
                {
                    this.OnPropertyChanged(nameof(this.Title));
                    (this.AddCarCommand as Command).ChangeCanExecute();
                    (this.UpdateCarCommand as Command).ChangeCanExecute();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}