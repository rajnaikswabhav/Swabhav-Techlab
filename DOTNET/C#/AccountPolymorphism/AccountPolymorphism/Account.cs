using System;

namespace AccountPolymorphism
{
    public abstract class Account
    {
        private readonly int _accountNumber;
        private readonly String _name;
        protected double balance;

        public Account(int accountNumber, String name, double balance)
        {
            _accountNumber = accountNumber;
            _name = name;
            this.balance = balance;
        }

        public void Deposit(double balance)
        {
            this.balance = this.balance + balance;
        }

        public abstract void WithDraw(double balance);

        public int AccountNumber
        {
            get
            {
                return _accountNumber;
            }
        }

        public String Name
        {
            get
            {
                return _name;
            }
        }

        public double Balance
        {
            get
            {
                return balance;
            }
        }
    }
}
