using System;
using System.Collections.Generic;
using System.Text;

namespace AccountPolymorphism
{
    class CurrentAccount : Account
    {
        public CurrentAccount(int accountNumber, string name, double balance) : base(accountNumber, name, balance)
        {
        }

        public override void WithDraw(double balance)
        {
            if (this.Balance >= 5000)
            {
                this.balance = this.balance - balance;
            }
            else
            {
                throw new Exception("Your Balance is low.Minimum balance require is 500..");
            }
        }
    }
}
