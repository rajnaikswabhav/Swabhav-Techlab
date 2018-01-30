using System;

namespace AccountApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Account account = new Account("Akash", 101, 2000);
            account.Deposit(2000);
            Console.WriteLine(account.Name+" Your balance is :"+account.Balance);
            account.Withdraw(1500);
            Console.WriteLine("After Withdrawing......");
            Console.WriteLine(account.Name + " Your balance is :" + account.Balance);

            Account account2 = new Account("Kartik",102,1000);
            account2.Deposit(500);
            Console.WriteLine(account2.Name + " Your balance is :" + account2.Balance);
            account2.Withdraw(500);
            Console.WriteLine("After Withdrawing......");
            Console.WriteLine(account2.Name + " Your balance is :" + account2.Balance);

            Console.WriteLine("Number Of Accounts :"+Account.GetNumberOfAccount());
        }
    }
}
