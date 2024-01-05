/*The Visitor pattern represents an operation to be performed 
on the elements of an object structure without changing the 
classes on which it operates. This pattern can be observed in 
the operation of a taxi company. When a person calls a taxi 
company (accepting a visitor), the company dispatches a cab to 
the customer. Upon entering the taxi the customer, or Visitor, 
is no longer in control of his or her own transportation, the taxi (driver) is.*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_8
{
    // Visitor Interface
    interface IVisitor1
    {
        void Visit(Employee employee);
    }

    // Element Interface
    interface IElement
    {
        void Accept(IVisitor1 visitor);
    }

    // Concrete Element
    class Employee : IElement
    {
        public string Name { get; }
        public double Salary { get; }

        public Employee(string name, double salary)
        {
            Name = name;
            Salary = salary;
        }

        public void Accept(IVisitor1 visitor)
        {
            visitor.Visit(this);
        }
    }

    // Concrete Visitor
    class SalaryRaiseVisitor : IVisitor1
    {
        public void Visit(Employee employee)
        {
            double newSalary = employee.Salary * 1.1; // 10% salary raise
            Console.WriteLine($"Increasing salary for {employee.Name} to {newSalary}");
        }
    }

    // Object Structure
    class EmployeesCollection
    {
        private List<Employee> employees = new List<Employee>();

        public void Attach(Employee employee)
        {
            employees.Add(employee);
        }

        public void Detach(Employee employee)
        {
            employees.Remove(employee);
        }

        public void Accept(IVisitor1 visitor)
        {
            foreach (var employee in employees)
            {
                employee.Accept(visitor);
            }
        }
    }
    // Visitor Interface
    interface IVisitor
    {
        void Visit(Circle circle);
        void Visit(Rectangle rectangle);
    }

    // Element Interface
    interface IShape
    {
        void Accept(IVisitor visitor);
    }

    // Concrete Elements
    class Circle : IShape
    {
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    class Rectangle : IShape
    {
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    // Concrete Visitor
    class AreaCalculator : IVisitor
    {
        public void Visit(Circle circle)
        {
            Console.WriteLine("Calculating area for Circle.");
        }

        public void Visit(Rectangle rectangle)
        {
            Console.WriteLine("Calculating area for Rectangle.");
        }
    }

    // Object Structure
    class Drawing
    {
        private List<IShape> shapes = new List<IShape>();

        public void AddShape(IShape shape)
        {
            shapes.Add(shape);
        }

        public void RemoveShape(IShape shape)
        {
            shapes.Remove(shape);
        }

        public void Accept(IVisitor visitor)
        {
            foreach (var shape in shapes)
            {
                shape.Accept(visitor);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------------------------------EXAMPLE 1---------------------------------");
            EmployeesCollection employees = new EmployeesCollection();
            employees.Attach(new Employee("Alice", 50000));
            employees.Attach(new Employee("Bob", 60000));
            employees.Attach(new Employee("Charlie", 70000));

            IVisitor1 salaryRaiseVisitor = new SalaryRaiseVisitor();
            employees.Accept(salaryRaiseVisitor);

            Console.WriteLine("---------------------------------EXAMPLE 2---------------------------------");
            Drawing drawing = new Drawing();
            drawing.AddShape(new Circle());
            drawing.AddShape(new Rectangle());

            IVisitor areaCalculator = new AreaCalculator();
            drawing.Accept(areaCalculator);
        }
    }

}

