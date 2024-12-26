using System;
using Moq;
using NUnit.Framework;

namespace LabWork6_TP
{
    public class BankAccount
    {
        public double balance = 0; private int accountID;
        public BankAccount(int accountID, double balance)
        {
            if (balance < 0) { throw new Exception("Unbelivable"); }
            else { this.balance = balance; this.accountID = accountID; } 
        }
        
    }
    public class BankAdmin
    {
        private IBankAccountDB db;
        public double getbalance(int id)
        {
            var balance = db.getbalance(id);
            if (balance < 0) { throw new Exception("Value ERROR"); }
            return balance;
        }
        public BankAdmin(IBankAccountDB db)
        {
            this.db = db;
        }
        public void deposit(int id, double amount)
        {
            var acc = db.account(id);
            if (amount <= 0 || acc is null) { throw new Exception("Value ERROR"); }
            else { acc.balance += amount; }
        }
        public void withdraw(int id, double amount)
        {
            var acc = db.account(id);
            if (amount <= 0 || acc is null) { throw new Exception("Value ERROR"); }
            else if (amount > getbalance(id)) { throw new Exception("Insufficient funds"); }
            else { acc.balance -= amount; }
        }
    }
    public interface IBankAccountDB 
    {
        double getbalance(int id);
        BankAccount? account(int id);
    }

    [TestFixture]
    public class BankAccountTest
    {
        private BankAdmin _admin;
        private Mock<IBankAccountDB> _mockBankAccount;

        [SetUp]
        public void SetUp()
        {
            _mockBankAccount = new Mock<IBankAccountDB>();
            _admin = new BankAdmin( _mockBankAccount.Object );
        }
        [Test]
        public void GetBalance_Success()
        {
            _mockBankAccount.Setup(p => p.getbalance(1)).Returns(100);

            var balance = _admin.getbalance(1);

            Assert.That(balance, Is.EqualTo(100));
            _mockBankAccount.Verify(r => r.getbalance(1), Times.Once());
        }
        [Test]
        public void GetBalance_Fail()
        {
            _mockBankAccount.Setup(p => p.getbalance(2)).Returns(-100);

            var balance = Assert.Throws<Exception>(() => _admin.getbalance(2));
            Assert.That(balance.Message, Is.EqualTo("Value ERROR"));
            _mockBankAccount.Verify(r => r.getbalance(2), Times.Once());
        }

        [Test]
        public void Deposit_Success()
        {
            _mockBankAccount.Setup(p => p.getbalance(3)).Returns(400);

            var balanceOld = _admin.getbalance(3);
            Assert.That(balanceOld, Is.EqualTo(400));
            _mockBankAccount.Verify(r => r.getbalance(3), Times.Once());

            BankAccount p1 = new BankAccount(3, 400);
            _mockBankAccount.Setup(p => p.account(3)).Returns(p1);
            _admin.deposit(3, 250);

            var balanceNew = p1.balance;
            Assert.That(balanceNew, Is.EqualTo(650));
            _mockBankAccount.Verify(r => r.account(3), Times.Once());
        }
        [Test]
        public void Deposit_Fail1()
        {
            _mockBankAccount.Setup(p => p.getbalance(3)).Returns(400);

            var balanceOld = _admin.getbalance(3);
            Assert.That(balanceOld, Is.EqualTo(400));
            _mockBankAccount.Verify(r => r.getbalance(3), Times.Once());

            BankAccount p2 = new BankAccount(3, 400);
            _mockBankAccount.Setup(p => p.account(3)).Returns(p2);
            
            var mess = Assert.Throws<Exception>(() => _admin.deposit(3, -250));
            Assert.That(mess.Message, Is.EqualTo("Value ERROR"));
            _mockBankAccount.Verify(r => r.getbalance(3), Times.Once());
        }
        [Test]
        public void Deposit_Fail2()
        {
            _mockBankAccount.Setup(p => p.getbalance(3)).Returns(400);

            var balanceOld = _admin.getbalance(3);
            Assert.That(balanceOld, Is.EqualTo(400));
            _mockBankAccount.Verify(r => r.getbalance(3), Times.Once());

            BankAccount p2 = new BankAccount(3, 400);
            _mockBankAccount.Setup(p => p.account(3)).Returns((BankAccount)null);

            var mess = Assert.Throws<Exception>(() => _admin.deposit(3, 250));
            Assert.That(mess.Message, Is.EqualTo("Value ERROR"));
            _mockBankAccount.Verify(r => r.getbalance(3), Times.Once());
        }
        [Test]
        public void Deposit_Fail3()
        {
            _mockBankAccount.Setup(p => p.getbalance(3)).Returns(400);

            var balanceOld = _admin.getbalance(3);
            Assert.That(balanceOld, Is.EqualTo(400));
            _mockBankAccount.Verify(r => r.getbalance(3), Times.Once());

            BankAccount p2 = new BankAccount(3, 400);
            _mockBankAccount.Setup(p => p.account(3)).Returns(p2);

            var mess = Assert.Throws<Exception>(() => _admin.deposit(3, 0));
            Assert.That(mess.Message, Is.EqualTo("Value ERROR"));
            _mockBankAccount.Verify(r => r.getbalance(3), Times.Once());
        }

        [Test]
        public void WithDraw_Success()
        {
            _mockBankAccount.Setup(p => p.getbalance(4)).Returns(1000);

            var balanceOld = _admin.getbalance(4);
            Assert.That(balanceOld, Is.EqualTo(1000));
            _mockBankAccount.Verify(r => r.getbalance(4), Times.Once());

            BankAccount p1 = new BankAccount(4, 1000);
            _mockBankAccount.Setup(p => p.account(4)).Returns(p1);
            _admin.withdraw(4, 501);

            var balanceNew = p1.balance;
            Assert.That(balanceNew, Is.EqualTo(499));
            _mockBankAccount.Verify(r => r.account(4), Times.Once());
        }
        [Test]
        public void Withdraw_Fail1()
        {
            _mockBankAccount.Setup(p => p.getbalance(3)).Returns(1000);

            var balanceOld = _admin.getbalance(3);
            Assert.That(balanceOld, Is.EqualTo(1000));
            _mockBankAccount.Verify(r => r.getbalance(3), Times.Once());

            BankAccount p2 = new BankAccount(3, 1000);
            _mockBankAccount.Setup(p => p.account(3)).Returns(p2);

            var mess = Assert.Throws<Exception>(() => _admin.withdraw(3, 0));
            Assert.That(mess.Message, Is.EqualTo("Value ERROR"));
            _mockBankAccount.Verify(r => r.getbalance(3), Times.Once());
        }
        [Test]
        public void Withdraw_Fail2()
        {
            _mockBankAccount.Setup(p => p.getbalance(3)).Returns(1000);

            var balanceOld = _admin.getbalance(3);
            Assert.That(balanceOld, Is.EqualTo(1000));
            _mockBankAccount.Verify(r => r.getbalance(3), Times.Once());

            BankAccount p2 = new BankAccount(3, 1000);
            _mockBankAccount.Setup(p => p.account(3)).Returns(p2);

            var mess = Assert.Throws<Exception>(() => _admin.withdraw(3, 1001));
            Assert.That(mess.Message, Is.EqualTo("Insufficient funds"));
            _mockBankAccount.Verify(r => r.account(3), Times.Once());
        }
        [Test]
        public void Withdraw_Fail3()
        {
            _mockBankAccount.Setup(p => p.getbalance(3)).Returns(1000);

            var balanceOld = _admin.getbalance(3);
            Assert.That(balanceOld, Is.EqualTo(1000));
            _mockBankAccount.Verify(r => r.getbalance(3), Times.Once());

            BankAccount p2 = new BankAccount(3, 1000);
            _mockBankAccount.Setup(p => p.account(3)).Returns(p2);

            var mess = Assert.Throws<Exception>(() => _admin.withdraw(3, -10));
            Assert.That(mess.Message, Is.EqualTo("Value ERROR"));
            _mockBankAccount.Verify(r => r.getbalance(3), Times.Once());
        }
        [Test]
        public void Deposit_Fail4()
        {
            _mockBankAccount.Setup(p => p.getbalance(3)).Returns(400);

            var balanceOld = _admin.getbalance(3);
            Assert.That(balanceOld, Is.EqualTo(400));
            _mockBankAccount.Verify(r => r.getbalance(3), Times.Once());

            BankAccount p2 = new BankAccount(3, 400);
            _mockBankAccount.Setup(p => p.account(3)).Returns((BankAccount)null);

            var mess = Assert.Throws<Exception>(() => _admin.withdraw(3, 250));
            Assert.That(mess.Message, Is.EqualTo("Value ERROR"));
            _mockBankAccount.Verify(r => r.getbalance(3), Times.Once());
        }

        [Test]
        public void Create_Success()
        {
            _mockBankAccount.Setup(p => p.getbalance(5)).Returns(5000);

            BankAccount b1 = new BankAccount(5, 5000);
            Assert.That(b1.balance, Is.EqualTo(_admin.getbalance(5)));
            _mockBankAccount.Verify(r => r.getbalance(5), Times.Once());
        }
        [Test]
        public void Create_Fail()
        {
            _mockBankAccount.Setup(p => p.getbalance(5)).Returns(-120);

            BankAccount b1;
            var mess = Assert.Throws<Exception>(() => b1 = new BankAccount(5, -120));
            Assert.That(mess.Message, Is.EqualTo("Unbelivable"));
        }
    }
}