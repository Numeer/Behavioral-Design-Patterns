/*A class of problems occurs repeatedly in a well-defined and well-understood domain.
If the domain were characterized with a "language", then problems could be easily solved with an interpretation "engine".
The pattern uses a class to represent each grammar rule. 
And since grammars are usually hierarchical in structure, an inheritance hierarchy of rule classes maps nicely.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_8
{

    // Abstract Expression
    interface IExpression1
    {
        int Interpret(Dictionary<string, int> context);
    }

    // Terminal Expression
    class NumberExpression : IExpression1
    {
        private readonly string variable;

        public NumberExpression(string variable)
        {
            this.variable = variable;
        }

        public int Interpret(Dictionary<string, int> context)
        {
            return context[variable];
        }
    }

    // Non-Terminal Expression
    class AdditionExpression : IExpression1
    {
        private readonly IExpression1 left;
        private readonly IExpression1 right;

        public AdditionExpression(IExpression1 left, IExpression1 right)
        {
            this.left = left;
            this.right = right;
        }

        public int Interpret(Dictionary<string, int> context)
        {
            return left.Interpret(context) + right.Interpret(context);
        }
    }
    // Abstract Expression
    interface IExpression
    {
        string Interpret(DateTime date);
    }

    // Terminal Expression
    class DayExpression : IExpression
    {
        public string Interpret(DateTime date)
        {
            return date.Day.ToString();
        }
    }

    class MonthExpression : IExpression
    {
        public string Interpret(DateTime date)
        {
            return date.ToString("MMMM");
        }
    }

    class YearExpression : IExpression
    {
        public string Interpret(DateTime date)
        {
            return date.Year.ToString();
        }
    }

    // Non-Terminal Expression
    class DateFormatExpression : IExpression
    {
        private readonly List<IExpression> expressions;

        public DateFormatExpression(List<IExpression> expressions)
        {
            this.expressions = expressions;
        }

        public string Interpret(DateTime date)
        {
            List<string> results = new List<string>();
            foreach (var expression in expressions)
            {
                results.Add(expression.Interpret(date));
            }
            return string.Join("-", results);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------------------------------EXAMPLE 1---------------------------------");
            // Context with variable assignments
            var context = new Dictionary<string, int>
            {
                { "a", 5 },
                { "b", 10 }
            };

            // Expression: a + b
            IExpression1 expression = new AdditionExpression(new NumberExpression("a"), new NumberExpression("b"));

            // Interpret expression
            int result = expression.Interpret(context);
            Console.WriteLine("Result: " + result); // Output: Result: 15

            Console.WriteLine("---------------------------------EXAMPLE 2---------------------------------");
            DateTime currentDate = DateTime.Now;

            // Expression: Day - Month - Year
            List<IExpression> expressions = new List<IExpression>
            {
                new DayExpression(),
                new MonthExpression(),
                new YearExpression()
            };
       
            IExpression dateFormatExpression = new DateFormatExpression(expressions);

            // Interpret expression
            string formattedDate = dateFormatExpression.Interpret(currentDate);
            Console.WriteLine("Formatted Date: " + formattedDate); // Output: Formatted Date: 27-November-2023
        }
    }

}

