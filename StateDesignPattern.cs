/*The State pattern allows an object to change its behavior
when its internal state changes. This pattern can be observed
in a vending machine. Vending machines have states based on 
the inventory, amount of currency deposited, the ability to make change,
the item selected, etc. When currency is deposited and a selection is made, 
a vending machine will either deliver a product and no change, 
deliver a product and change, deliver no product due to insufficient 
currency on deposit, or deliver no product due to inventory depletion.*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_8
{
    // State Interface
    interface ITrafficLightState
    {
        void ChangeState(TrafficLightController controller);
    }

    // Concrete States
    class RedState : ITrafficLightState
    {
        public void ChangeState(TrafficLightController controller)
        {
            Console.WriteLine("Changing state to Green");
            controller.SetState(new GreenState());
        }
    }

    class GreenState : ITrafficLightState
    {
        public void ChangeState(TrafficLightController controller)
        {
            Console.WriteLine("Changing state to Red");
            controller.SetState(new RedState());
        }
    }

    // Context (Context class)
    class TrafficLightController
    {
        private ITrafficLightState currentState;

        public TrafficLightController()
        {
            currentState = new RedState(); // Initial state
        }

        public void SetState(ITrafficLightState state)
        {
            currentState = state;
        }

        public void ChangeState()
        {
            currentState.ChangeState(this);
        }
    }
    // State Interface
    interface IATMState
    {
        void InsertCard();
        void EjectCard();
        void EnterPin(int pin);
        void WithdrawCash(int amount);
    }

    // Concrete States
    class NoCardState : IATMState
    {
        public void InsertCard()
        {
            Console.WriteLine("Card inserted. Please enter your PIN.");
        }

        public void EjectCard()
        {
            Console.WriteLine("No card to eject.");
        }

        public void EnterPin(int pin)
        {
            Console.WriteLine("Please insert a card first.");
        }

        public void WithdrawCash(int amount)
        {
            Console.WriteLine("Please insert a card first.");
        }
    }

    class CardInsertedState : IATMState
    {
        public void InsertCard()
        {
            Console.WriteLine("Card is already inserted.");
        }

        public void EjectCard()
        {
            Console.WriteLine("Card ejected.");
        }

        public void EnterPin(int pin)
        {
            Console.WriteLine("PIN entered. You can now withdraw cash.");
        }

        public void WithdrawCash(int amount)
        {
            Console.WriteLine($"Withdrawn {amount} from ATM.");
        }
    }

    // Context (Context class)
    class ATM
    {
        private IATMState currentState;

        public ATM()
        {
            currentState = new NoCardState(); // Initial state
        }

        public void SetState(IATMState state)
        {
            currentState = state;
        }

        public void InsertCard()
        {
            currentState.InsertCard();
            if (currentState is NoCardState)
                SetState(new CardInsertedState());
        }

        public void EjectCard()
        {
            currentState.EjectCard();
            if (currentState is CardInsertedState)
                SetState(new NoCardState());
        }

        public void EnterPin(int pin)
        {
            currentState.EnterPin(pin);
        }

        public void WithdrawCash(int amount)
        {
            currentState.WithdrawCash(amount);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------------------------------EXAMPLE 1---------------------------------");
            TrafficLightController trafficLight = new TrafficLightController();

            trafficLight.ChangeState(); 
            trafficLight.ChangeState(); 
            trafficLight.ChangeState();


            Console.WriteLine("---------------------------------EXAMPLE 2---------------------------------");
            ATM atm = new ATM();

            atm.InsertCard(); // Card inserted. Please enter your PIN.
            atm.EnterPin(1234); // PIN entered. You can now withdraw cash.
            atm.WithdrawCash(1000); // Withdrawn 1000 from ATM.
            atm.EjectCard();
        }
    }

}

