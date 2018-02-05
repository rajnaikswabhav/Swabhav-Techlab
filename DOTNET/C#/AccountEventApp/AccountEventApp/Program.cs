using System;
using System.Collections.Generic;
using System.Text;

namespace AccountEventApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var account1 = new Account("Akash",101,2000);
            account1.OnBalanceChange += (a) => { Console.WriteLine("{0} your balance is change,"
                +"Now your balance is {1}.Informed By Email.", a.Name, a.Balance); };
            account1.OnBalanceChange += (a) => { Console.WriteLine("{0} your balance is change,"
                + "Now your balance is {1}.Informed By SMS.", a.Name, a.Balance);
            };

            //account1.OnBalanceChange += SendEmail;
            //account1.OnBalanceChange += SendSMS;

            account1.Deposit(3000);
            account1.Withdraw(4000);
        }

        public static void SendEmail(Account account)
        {
            Console.WriteLine("{0} your balance is change,Now your balance is {1}.Informed By Email.",account.Name,account.Balance);
        }

        public static void SendSMS(Account account)
        {
            Console.WriteLine("{0} your balance is change,Now your balance is {1}.Informed By SMS.", account.Name, account.Balance);
        }
    }
}
