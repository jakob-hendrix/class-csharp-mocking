using NUnit.Framework;
using Loans.Domain.Applications;
using Moq;

namespace Loans.Tests
{
    public class LoanApplicationProcessorShould
    {
        [Test]
        public void AcceptMinimumSalary()
        {
            LoanProduct product = new LoanProduct(99, "Load", 5.25M);
            LoanAmount amount = new LoanAmount("USD", 200_000);
            var application = new LoanApplication(42,
                product,
                amount,
                "Sarah",
                25,
                "133 Fake Dr",
                65_000);

            var mockIdentityVerifier = new Mock<IIdentityVerifier>();
            var mockCreditScorer = new Mock<ICreditScorer>();

            // how to isolate the dependencies? Passing nulls cause this to fail.
            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object,
                mockCreditScorer.Object);

            sut.Process(application);

            Assert.That(application.GetIsAccepted(), Is.True);
        }

        [Test]
        public void DeclineLowSalary()
        { 
            LoanProduct product = new LoanProduct(99, "Load", 5.25M);
            LoanAmount amount = new LoanAmount("USD",200_000);
            var application = new LoanApplication(42,
                product,
                amount,
                "Sarah",
                25,
                "133 Fake Dr",
                64_999);

            var mockIdentityVerifier = new Mock<IIdentityVerifier>();
            var mockCreditScorer = new Mock<ICreditScorer>();

            // how to isolate the dependencies? Passing nulls cause this to fail.
            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object,
                                                   mockCreditScorer.Object);

            sut.Process(application);

            Assert.That(application.GetIsAccepted(),Is.False);
        }
    }
}
