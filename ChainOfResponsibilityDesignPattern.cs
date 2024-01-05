// Chain of Responsibility simplifies object interconnections. 
// Instead of senders and receivers maintaining references to all candidate receivers, 
// each sender keeps a single reference to the head of the chain,
// and each receiver keeps a single reference to its immediate successor in the chain.
// Such as interviewing process, if the first interviewer cannot approve the candidate,
// then the candidate will be send back.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_8
{
    // Handler Interface
    interface IWithdrawalHandler
    {
        void SetNextHandler(IWithdrawalHandler nextHandler);
        void HandleRequest(int amount);
    }

    // Concrete Handler
    class FiftyDollarHandler : IWithdrawalHandler
    {
        private IWithdrawalHandler nextHandler;

        public void SetNextHandler(IWithdrawalHandler nextHandler)
        {
            this.nextHandler = nextHandler;
        }

        public void HandleRequest(int amount)
        {
            if (amount >= 50)
            {
                int numFiftyBills = amount / 50;
                int remainingAmount = amount % 50;
                Console.WriteLine($"Dispensing {numFiftyBills} $50 bills");
                if (remainingAmount > 0 && nextHandler != null)
                {
                    nextHandler.HandleRequest(remainingAmount);
                }
            }
            else if (nextHandler != null)
            {
                nextHandler.HandleRequest(amount);
            }
            else
            {
                Console.WriteLine("Cannot process the request.");
            }
        }
    }

    class ATM
    {
        private IWithdrawalHandler withdrawalHandler;

        public ATM()
        {
            InitializeHandlers();
        }

        private void InitializeHandlers()
        {
            IWithdrawalHandler fiftyHandler = new FiftyDollarHandler();
            withdrawalHandler = fiftyHandler;

            // Add more handlers if needed
            // e.g., withdrawalHandler.SetNextHandler(new HundredDollarHandler());
        }

        public void Withdraw(int amount)
        {
            withdrawalHandler.HandleRequest(amount);
        }
    }
    // Handler Interface
    interface IApprovalHandler
    {
        void SetNextHandler(IApprovalHandler nextHandler);
        void ApproveRequest(int amount);
    }

    // Concrete Handler
    class ManagerHandler : IApprovalHandler
    {
        private const int MAX_APPROVAL_AMOUNT = 1000;
        private IApprovalHandler nextHandler;

        public void SetNextHandler(IApprovalHandler nextHandler)
        {
            this.nextHandler = nextHandler;
        }

        public void ApproveRequest(int amount)
        {
            if (amount <= MAX_APPROVAL_AMOUNT)
            {
                Console.WriteLine($"Manager approves the request for ${amount}");
            }
            else if (nextHandler != null)
            {
                Console.WriteLine($"Manager cannot approve ${amount}. Passing to higher authority.");
                nextHandler.ApproveRequest(amount);
            }
            else
            {
                Console.WriteLine("Request exceeds approval limits.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------------------------------EXAMPLE 1---------------------------------");
            ATM atm = new ATM();
            atm.Withdraw(175);


            Console.WriteLine("---------------------------------EXAMPLE 2---------------------------------");
            IApprovalHandler manager = new ManagerHandler();

            manager.ApproveRequest(500);
            manager.ApproveRequest(1500);
        }
    }

}

