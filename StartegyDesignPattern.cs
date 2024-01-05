/*A Strategy defines a set of algorithms that can be used interchangeably.
 Modes of transportation to an airport is an example of a Strategy.
 Several options exist such as driving one's own car, taking a taxi, 
 an airport shuttle, a city bus, or a limousine service.
 The traveler must choose the Strategy based on trade-offs
 between cost, convenience, and time.*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_8
{
    // Strategy Interface
    interface ISortStrategy
    {
        void Sort(List<int> list);
    }

    // Concrete Strategies
    class BubbleSortStrategy : ISortStrategy
    {
        public void Sort(List<int> list)
        {
            Console.WriteLine("Sorting using Bubble Sort");
            // Perform Bubble Sort algorithm
            list.Sort();
        }
    }

    class MergeSortStrategy : ISortStrategy
    {
        public void Sort(List<int> list)
        {
            Console.WriteLine("Sorting using Merge Sort");
            // Perform Merge Sort algorithm
            list.Sort();
            // Additional steps for Merge Sort
        }
    }

    // Context (Context class)
    class SortContext
    {
        private ISortStrategy sortStrategy;

        public void SetSortStrategy(ISortStrategy strategy)
        {
            sortStrategy = strategy;
        }

        public void SortList(List<int> list)
        {
            sortStrategy.Sort(list);
        }
    }
    // Strategy Interface
    interface IPaymentStrategy
    {
        void Pay(int amount);
    }

    // Concrete Strategies
    class CreditCardPayment : IPaymentStrategy
    {
        private string cardNumber;
        private string cvv;

        public CreditCardPayment(string cardNumber, string cvv)
        {
            this.cardNumber = cardNumber;
            this.cvv = cvv;
        }

        public void Pay(int amount)
        {
            Console.WriteLine($"Paying ${amount} using Credit Card ({cardNumber})");
            // Process payment using credit card details
        }
    }

    class PayPalPayment : IPaymentStrategy
    {
        private string email;
        private string password;

        public PayPalPayment(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

        public void Pay(int amount)
        {
            Console.WriteLine($"Paying ${amount} using PayPal ({email})");
            // Process payment using PayPal credentials
        }
    }

    // Context (Context class)
    class PaymentContext
    {
        private IPaymentStrategy paymentStrategy;

        public void SetPaymentStrategy(IPaymentStrategy strategy)
        {
            paymentStrategy = strategy;
        }

        public void MakePayment(int amount)
        {
            paymentStrategy.Pay(amount);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------------------------------EXAMPLE 1---------------------------------");
            List<int> numbers = new List<int> { 7, 2, 5, 1, 9 };

            SortContext sortContext = new SortContext();

            // Using Bubble Sort Strategy
            sortContext.SetSortStrategy(new BubbleSortStrategy());
            sortContext.SortList(numbers);

            Console.WriteLine("After sorting:");
            Console.WriteLine(string.Join(", ", numbers));

            // Using Merge Sort Strategy
            sortContext.SetSortStrategy(new MergeSortStrategy());
            sortContext.SortList(numbers);

            Console.WriteLine("After sorting:");
            Console.WriteLine(string.Join(", ", numbers));

            Console.WriteLine("---------------------------------EXAMPLE 2---------------------------------");
            PaymentContext paymentContext = new PaymentContext();

            // Using Credit Card Payment Strategy
            paymentContext.SetPaymentStrategy(new CreditCardPayment("1234-5678-9012-3456", "123"));
            paymentContext.MakePayment(100);

            // Using PayPal Payment Strategy
            paymentContext.SetPaymentStrategy(new PayPalPayment("example@example.com", "password"));
            paymentContext.MakePayment(50);
        }
    }

}

