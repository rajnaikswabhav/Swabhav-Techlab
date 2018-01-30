using System;


namespace AccountPolymorphism
{
    class Program
    {
        static void Main(string[] args)
        {
            SavingAccount savingAccount = new SavingAccount(101, "xyz", 5000);
            savingAccount.Deposit(1000);
            savingAccount.WithDraw(5000);
            PrintDetails(savingAccount);

            CurrentAccount currentAccount = new CurrentAccount(102, "abc", 10000);
            currentAccount.Deposit(2000);
            currentAccount.WithDraw(9000);
            PrintDetails(currentAccount);
        }

        static void PrintDetails(Account account)
        {
            Console.WriteLine("Account NUmber is :" + account.AccountNumber);
            Console.WriteLine("AccountHolder Name is :" + account.Name);
            Console.WriteLine("Balance is :" + account.Balance);
            Console.WriteLine();
        }
    }
}
