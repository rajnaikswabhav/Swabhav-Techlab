using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AccountApp;

namespace AccountCore.Test
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void ShouldInstiateAccountThroughConstructor()
        {
            Account account = new Account("Akash", 101, 5000);

            Assert.AreEqual(account.Name, "Akash");
            Assert.AreEqual(account.Id,101);
            Assert.AreEqual(account.Balance,5000);
        }

        [TestMethod]
        public void ShouldInstiateDepositMethod()
        {
            double balance = 2000;
            Account acc = new Account("Akash",101,5000);
            acc.Deposit(balance);

            Assert.AreEqual(acc.Balance, 7000);
        }

        [TestMethod]
        public void ShouldInstiateWithdrawMethod()
        {
            double balance = 2000;
            Account acc = new Account("Akash",101,5000);
            acc.Withdraw(balance);

            Assert.AreEqual(acc.Balance , 3000);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),"Your balance is low..")]
        public void ShouldThrowExceptionInWithdrawMwthod()
        {
            double balance = 4500;
            Account acc = new Account("Akash",101,5000);
            acc.Withdraw(balance);
            Assert.AreEqual(acc.Balance , 500);
        }
    }
}
