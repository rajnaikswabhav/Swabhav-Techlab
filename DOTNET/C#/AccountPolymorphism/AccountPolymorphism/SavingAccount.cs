using System;

namespace AccountPolymorphism
{
    class SavingAccount : Account
    {
        public SavingAccount(int accountNumber, string name, double balance) : base(accountNumber, name, balance)
        {
        }

        public override void WithDraw(double balance)
        {
            if (this.balance >= 500 && this.balance - balance > 500)
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
