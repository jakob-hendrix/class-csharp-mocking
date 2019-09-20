using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Loans.Domain.Applications;

namespace Loans.Tests
{
    public class LoanApplicationProcessorShould
    {
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

            // how to isolate the dependencies? Passing nulls cause this to fail.
            var sut = new LoanApplicationProcessor(null, null);

            sut.Process(application);

            Assert.That(application.GetIsAccepted(),Is.False);
        }
    }
}
