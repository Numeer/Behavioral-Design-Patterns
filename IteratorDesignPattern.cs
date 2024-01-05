/*The Iterator provides ways to access elements of an aggregate object 
sequentially without exposing the underlying structure of the object.
Consider watching television in a hotel room in a strange city.
When surfing through channels, the channel number is not important, 
but the programming is. If the programming on one channel is not of interest, 
the viewer can request the next channel, without knowing its number.
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_8
{

    // Aggregate Interface
    interface IAbstractCollection
    {
        Iterator CreateIterator();
    }

    // Concrete Aggregate
    class ConcreteCollection : IAbstractCollection
    {
        private ArrayList items = new ArrayList();

        public Iterator CreateIterator()
        {
            return new ConcreteIterator(this);
        }

        public int Count => items.Count;

        public object this[int index]
        {
            get => items[index];
            set => items.Insert(index, value);
        }
    }

    // Iterator Interface
    interface Iterator
    {
        object First();
        object Next();
        bool IsDone();
        object CurrentItem();
    }

    // Concrete Iterator
    class ConcreteIterator : Iterator
    {
        private ConcreteCollection collection;
        private int currentPosition = 0;

        public ConcreteIterator(ConcreteCollection collection)
        {
            this.collection = collection;
        }

        public object First()
        {
            currentPosition = 0;
            return collection[currentPosition];
        }

        public object Next()
        {
            currentPosition++;
            return IsDone() ? null : collection[currentPosition];
        }

        public bool IsDone()
        {
            return currentPosition >= collection.Count - 1;
        }

        public object CurrentItem()
        {
            return collection[currentPosition];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------------------------------EXAMPLE 1---------------------------------");
           /* ConcreteCollection collection = new ConcreteCollection();
            collection[0] = "Item 1";
            collection[1] = "Item 2";
            collection[2] = "Item 3";

            Iterator iterator = collection.CreateIterator();

            Console.WriteLine("Iterating through the collection:");
            for (object item = iterator.First(); !iterator.IsDone(); item = iterator.Next())
            {
                Console.WriteLine(item);
            }*/

            Console.WriteLine("---------------------------------EXAMPLE 2---------------------------------");
            List<string> items = new List<string> { "Item 1", "Item 2", "Item 3" };

            IEnumerator<string> iterator = items.GetEnumerator();

            Console.WriteLine("Iterating through the collection:");
            while (iterator.MoveNext())
            {
                Console.WriteLine(iterator.Current);
            }

        }
    }

}

