﻿using System.Collections.ObjectModel;
using DataBindingDemo.Model;

namespace DataBindingDemo.ViewModels
{
    public class CarsViewModel
    {
        public CarsViewModel()
        {
            this.Cars = new ObservableCollection<Car>
            {
                new Car
                {
                    Brand = "BMW",
                    Model = "X5"
                },
                new Car
                {
                    Brand = "Audi",
                    Model = "A4"
                },
            };
        }
        public string Title { get; } = "Simple Cars";

        public ObservableCollection<Car> Cars { get; set; }
    }
}