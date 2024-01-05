/*The Template Method defines a skeleton of an algorithm in an operation, 
and defers some steps to subclasses. Home builders use the Template Method 
when developing a new subdivision. A typical subdivision consists of a limited 
number of floor plans with different variations available for each. Within a
floor plan, the foundation, framing, plumbing, and wiring will be identical 
for each house. Variation is introduced in the later stages of construction to produce a wider variety of models.*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_8
{

    // Abstract class defining the template method
    abstract class HouseBuilder
    {
        public void BuildHouse()
        {
            BuildFoundation();
            BuildWalls();
            BuildRoof();
            AddFurniture();
        }

        protected abstract void BuildFoundation();
        protected abstract void BuildWalls();
        protected abstract void BuildRoof();

        // Hook method - subclasses may override it
        protected virtual void AddFurniture()
        {
            Console.WriteLine("Adding basic furniture to the house.");
        }
    }

    // Concrete class implementing the HouseBuilder template
    class ConcreteHouseBuilder : HouseBuilder
    {
        protected override void BuildFoundation()
        {
            Console.WriteLine("Building a strong foundation for the house.");
        }

        protected override void BuildWalls()
        {
            Console.WriteLine("Constructing walls for the house.");
        }

        protected override void BuildRoof()
        {
            Console.WriteLine("Adding a roof to the house.");
        }

        // Overriding the hook method
        protected override void AddFurniture()
        {
            Console.WriteLine("Adding luxury furniture to the house.");
        }
    }
    // Abstract class defining the template method
    abstract class OnlineOrdering
    {
        public void ProcessOrder()
        {
            DisplayProducts();
            AddToCart();
            ApplyDiscount();
            Checkout();
        }

        protected abstract void DisplayProducts();
        protected abstract void AddToCart();
        protected abstract void Checkout();

        // Hook method - subclasses may override it
        protected virtual void ApplyDiscount()
        {
            Console.WriteLine("Applying default discount.");
        }
    }

    // Concrete class implementing the OnlineOrdering template
    class CustomerOrder : OnlineOrdering
    {
        protected override void DisplayProducts()
        {
            Console.WriteLine("Displaying available products for the customer.");
        }

        protected override void AddToCart()
        {
            Console.WriteLine("Adding selected items to the cart.");
        }

        protected override void Checkout()
        {
            Console.WriteLine("Processing customer's payment and finalizing the order.");
        }

        // Overriding the hook method
        protected override void ApplyDiscount()
        {
            Console.WriteLine("Applying customer-specific discount.");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------------------------------EXAMPLE 1---------------------------------");
            HouseBuilder houseBuilder = new ConcreteHouseBuilder();
            houseBuilder.BuildHouse();


            Console.WriteLine("---------------------------------EXAMPLE 2---------------------------------");
            OnlineOrdering order = new CustomerOrder();
            order.ProcessOrder();

        }
    }

}

