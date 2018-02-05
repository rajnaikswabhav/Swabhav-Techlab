using System;
using System.Collections.Generic;
using System.Text;

namespace AccountEventApp
{
    //public delegate void DBalanceModified(Account account);
    public class Account
    {
        private readonly String name;
        private readonly int accountId;
        private double balance;
        public event Action<Account> OnBalanceChange = null;
        //public event DBalanceModified OnBalanceChange = null; 
        public Account(String name, int accountId, double balance)
        {
            this.name = name;
            this.accountId = accountId;
            this.balance = balance;
        }

        public void Deposit(double balance)
        {
            this.balance = this.balance + balance;
            if (OnBalanceChange != null)
            {
                OnBalanceChange(this);
            }
        }

        public void Withdraw(double balance)
        {
            if (this.balance > 500 && this.balance - balance >= 500)
            {
                this.balance = this.balance - balance;
                if (OnBalanceChange != null)
                {
                    OnBalanceChange(this);
                }
            }
            else
            {
                throw new Exception("Your balance is low....");
            }
        }

        public String Name { get { return name; } }
        public int AccountId { get { return accountId; } }
        public double Balance { get { return balance; } }
    }
}
