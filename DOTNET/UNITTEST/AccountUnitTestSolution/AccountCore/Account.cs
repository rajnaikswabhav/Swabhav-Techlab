using System;

namespace AccountApp
{
    public class Account
    {
        private readonly String _name;
        private readonly int _id;
        private double _balance;
        private static int count = 0;
        private static readonly int MINIMUM_BALANCE = 500;

        public Account(String name, int id, double balance)
        {
            _name = name;
            _id = id;
            _balance = balance;
            count++;
        }

        public String Name
        {
            get
            {
                return _name;
            }
        }

        public int Id
        {
            get
            {
                return _id;
            }
        }

        public double Balance
        {
            get
            {
                return _balance;
            }
        }

        public void Deposit(double balance)
        {
            _balance = _balance + balance;
        }

        public void Withdraw(double balance)
        {
            if (_balance >= MINIMUM_BALANCE && _balance - balance > MINIMUM_BALANCE)
            {
                _balance = _balance - balance;
            }
            else
            {
                throw new Exception
                    ("Your balance is low,Minimum require balance is 500.");
            }
        }

        public static int GetNumberOfAccount()
        {
            return count;
        }

    }
}