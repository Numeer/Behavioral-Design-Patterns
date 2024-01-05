/*The Observer defines a one-to-many relationship so that when 
one object changes state, the others are notified and updated automatically. 
Subscriber and non subscribers when someone unsubscribe he/she 
didin't notified with any notifications*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_8
{
    // Subject (Observable) Interface
    interface ISubject
    {
        void RegisterObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers();
    }

    // Concrete Subject
    class WeatherStation : ISubject
    {
        private double temperature;
        private List<IObserver> observers = new List<IObserver>();

        public double Temperature
        {
            get => temperature;
            set
            {
                temperature = value;
                NotifyObservers();
            }
        }

        public void RegisterObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in observers)
            {
                observer.Update(Temperature);
            }
        }
    }

    // Observer Interface
    interface IObserver
    {
        void Update(double temperature);
    }

    // Concrete Observer
    class WeatherDisplay : IObserver
    {
        public void Update(double temperature)
        {
            Console.WriteLine($"Weather Display: Temperature is {temperature} degrees Celsius");
        }
    }
    // Subject (Observable) Interface
    interface IStockMarket
    {
        void RegisterObserver(IInvestor observer);
        void RemoveObserver(IInvestor observer);
        void NotifyObservers(double price);
    }

    // Concrete Subject
    class StockMarket : IStockMarket
    {
        private double stockPrice;
        private List<IInvestor> investors = new List<IInvestor>();

        public double StockPrice
        {
            get => stockPrice;
            set
            {
                stockPrice = value;
                NotifyObservers(stockPrice);
            }
        }

        public void RegisterObserver(IInvestor investor)
        {
            investors.Add(investor);
        }

        public void RemoveObserver(IInvestor investor)
        {
            investors.Remove(investor);
        }

        public void NotifyObservers(double price)
        {
            foreach (var investor in investors)
            {
                investor.Update(price);
            }
        }
    }

    // Observer Interface
    interface IInvestor
    {
        void Update(double stockPrice);
    }

    // Concrete Observer
    class StockInvestor : IInvestor
    {
        private string name;

        public StockInvestor(string name)
        {
            this.name = name;
        }

        public void Update(double stockPrice)
        {
            Console.WriteLine($"{name}: Stock price updated to {stockPrice}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------------------------------EXAMPLE 1---------------------------------");
            WeatherStation weatherStation = new WeatherStation();
            WeatherDisplay weatherDisplay = new WeatherDisplay();

            weatherStation.RegisterObserver(weatherDisplay);

            weatherStation.Temperature = 25.5;
            weatherStation.Temperature = 30.0;

            Console.WriteLine("---------------------------------EXAMPLE 2---------------------------------");
            StockMarket stockMarket = new StockMarket();
            StockInvestor investor1 = new StockInvestor("Alice");
            StockInvestor investor2 = new StockInvestor("Bob");

            stockMarket.RegisterObserver(investor1);
            stockMarket.RegisterObserver(investor2);

            stockMarket.StockPrice = 150.0;
            stockMarket.StockPrice = 160.0;
        }
    }

}

