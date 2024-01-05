/*The Mediator defines an object that controls how a set of objects interact. 
Loose coupling between colleague objects is achieved by having colleagues 
communicate with the Mediator, rather than with each other. The control 
tower at a controlled airport demonstrates this pattern very well. 
The pilots of the planes approaching or departing the terminal area 
communicate with the tower rather than explicitly communicating with one another. 
The constraints on who can take off or land are enforced by the tower. 
It is important to note that the tower does not control the whole flight. 
It exists only to enforce constraints in the terminal area.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_8
{

    // Mediator Interface
    interface IChatRoom
    {
        void SendMessage(string message, Participant participant);
    }

    // Concrete Mediator
    class ChatRoom : IChatRoom
    {
        private readonly List<Participant> participants = new List<Participant>();

        public void SendMessage(string message, Participant participant)
        {
            foreach (var p in participants)
            {
                if (p != participant)
                {
                    p.Receive(message);
                }
            }
        }

        public void Register(Participant participant)
        {
            participants.Add(participant);
        }
    }

    // Colleague (Participant) class
    abstract class Participant
    {
        protected readonly IChatRoom chatRoom;
        public string Name { get; }

        public Participant(IChatRoom chatRoom, string name)
        {
            this.chatRoom = chatRoom;
            this.Name = name;
        }

        public void Send(string message)
        {
            Console.WriteLine($"{Name} sends: {message}");
            chatRoom.SendMessage(message, this);
        }

        public abstract void Receive(string message);
    }

    // Concrete Colleague classes
    class User : Participant
    {
        public User(IChatRoom chatRoom, string name) : base(chatRoom, name) { }

        public override void Receive(string message)
        {
            Console.WriteLine($"{Name} receives: {message}");
        }
    }
    // Mediator Interface
    interface IAirTrafficControl
    {
        void RegisterFlight(Flight flight);
        void SendMessage(string message, Flight sender);
    }

    // Concrete Mediator
    class AirTrafficControl : IAirTrafficControl
    {
        private readonly List<Flight> flights = new List<Flight>();

        public void RegisterFlight(Flight flight)
        {
            flights.Add(flight);
        }

        public void SendMessage(string message, Flight sender)
        {
            foreach (var flight in flights)
            {
                if (flight != sender)
                {
                    flight.Receive(message);
                }
            }
        }
    }

    // Colleague (Flight) class
    class Flight
    {
        private readonly IAirTrafficControl airTrafficControl;
        public string FlightCode { get; }

        public Flight(IAirTrafficControl airTrafficControl, string flightCode)
        {
            this.airTrafficControl = airTrafficControl;
            this.FlightCode = flightCode;
            airTrafficControl.RegisterFlight(this);
        }

        public void Send(string message)
        {
            Console.WriteLine($"Flight {FlightCode} sends: {message}");
            airTrafficControl.SendMessage(message, this);
        }

        public void Receive(string message)
        {
            Console.WriteLine($"Flight {FlightCode} receives: {message}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------------------------------EXAMPLE 1---------------------------------");
            ChatRoom chatRoom = new ChatRoom();

            User user1 = new User(chatRoom, "Alice");
            User user2 = new User(chatRoom, "Bob");
            User user3 = new User(chatRoom, "Charlie");

            chatRoom.Register(user1);
            chatRoom.Register(user2);
            chatRoom.Register(user3);

            user1.Send("Hello, everyone!");

            Console.WriteLine("---------------------------------EXAMPLE 2---------------------------------");
            AirTrafficControl atc = new AirTrafficControl();

            Flight flight1 = new Flight(atc, "ABC123");
            Flight flight2 = new Flight(atc, "XYZ789");

            flight1.Send("Traffic information for ABC123");
        }
    }

}

