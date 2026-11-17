using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp12E
{
    [TestFixture]
    public class BankTest
    {
        Bank bank;

        [SetUp]
        public void SetUp()
        {
            bank = new Bank();
            bank.CreateAccount("1234", "Gipsz Jakab");
        }

        [Test]
        public void StartingBalanceIsZero()
        {
            Assert.That(bank.GetBalance("1234"), Is.Zero);
        }

        [Test]
        public void DepositOnce()
        {
            bank.Deposit("1234", 10000);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(10000));
        }

        [Test]
        public void DepositMultiple()
        {
            bank.Deposit("1234", 10000);
            bank.Deposit("1234", 15000);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(25000));
        }

        [Test]
        public void DepositNegativeAmount()
        {
            Assert.That(() => bank.Deposit("1234", -10000), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void DepositMultipleAccounts()
        {
            bank.CreateAccount("5678", "Gipsz Jakab");
            bank.Deposit("1234", 10000);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(10000));
            Assert.That(bank.GetBalance("5678"), Is.Zero);
        }

        [Test]
        public void GetBalanceOfInvalidAccount()
        {
            Assert.That(() => bank.GetBalance("5678"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void DepositToInvalidAccount()
        {
            Assert.That(() => bank.Deposit("5678", 5000), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void WithdrawFromAccount()
        {
            bank.Deposit("1234", 10000);
            bank.Withdraw("1234", 5000);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(5000));
        }

        [Test]
        public void WithdrawFromInvalidAccount()
        {
            Assert.That(() => bank.Withdraw("5678", 500), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void WithdrawNegativeAmount()
        {
            Assert.That(() => bank.Withdraw("1234", -500), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void WithdrawMoreThanBalance()
        {
            bank.Deposit("1234", 10000);
            Assert.That(bank.Withdraw("1234", 15000), Is.False);
        }

        [Test]
        public void WithdrawMultipleTimes()
        {
            bank.Deposit("1234", 10000);
            bank.Withdraw("1234", 5000);
            bank.Withdraw("1234", 2000);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(3000));
        }

        [Test]
        public void TransferBetweenAccounts()
        {
            bank.CreateAccount("5678", "Gipsz Jakab");
            bank.Deposit("1234", 10000);
            bank.Transfer("1234", "5678", 5000);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(5000));
            Assert.That(bank.GetBalance("5678"), Is.EqualTo(5000));
        }

        [Test]
        public void TransferFromInvalidAccount()
        {
            bank.CreateAccount("5678", "Gipsz Jakab");
            Assert.That(() => bank.Transfer("9999", "5678", 5000), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TransferToInvalidAccount()
        {
            bank.CreateAccount("5678", "Gipsz Jakab");
            Assert.That(() => bank.Transfer("1234", "9999", 5000), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TransferNegativeAmount()
        {
            bank.CreateAccount("5678", "Gipsz Jakab");
            Assert.That(() => bank.Transfer("1234", "5678", -5000), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void TransferMoreThanBalance()
        {
            bank.CreateAccount("5678", "Gipsz Jakab");
            bank.Deposit("1234", 10000);
            Assert.That(() => bank.Transfer("1234", "5678", 15000), Is.False);
        }

        [Test]
        public void TransferMultipleTimes()
        {
            bank.CreateAccount("5678", "Gipsz Jakab");
            bank.Deposit("1234", 10000);
            bank.Transfer("1234", "5678", 5000);
            bank.Transfer("5678", "1234", 2000);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(7000));
            Assert.That(bank.GetBalance("5678"), Is.EqualTo(3000));
        }

        [Test]
        public void CloseAccountWithBalance()
        {
            bank.Deposit("1234", 10000);
            Assert.That(() => bank.CloseAccount("1234"), Throws.TypeOf<InvalidOperationException>());
        }

        [Test] 
        public void CloseInvalidAccount()
        {
            Assert.That(() => bank.CloseAccount("5678"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void CloseAccountWithZeroBalance()
        {
            bank.CloseAccount("1234");
            Assert.That(() => bank.GetBalance("1234"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void GetAccountHolderName()
        {
            Assert.That(bank.GetOwner("1234"), Is.EqualTo("Gipsz Jakab"));
        }

        [Test]
        public void GetAccountHolderNameOfInvalidAccount()
        {
            Assert.That(() => bank.GetOwner("5678"), Throws.TypeOf<ArgumentException>());
        }

    }
}
