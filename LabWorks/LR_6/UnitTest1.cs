using Moq;
namespace Test
{
    public class Tests
    {
        public class BankAccountTest
        {
            private BankAccount _account;
            private Mock<BankAccount> _mockBankAccount;

            [SetUp]
            public void SetUp()
            {
                _mockBankAccount = new Mock<BankAccount>();
                _account = new BankAccount(1, 0);
            }
            [Test]
            public void Deposit_Success()
            {
                _mockBankAccount.Setup(p => p.getbalance()).Returns(1000.42);

                var dep = _account.getbalance();

                Assert.That(dep, Is.EqualTo(1000.42));
            }
        }
    }
}