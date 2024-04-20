using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using csharp_otp_2019;

namespace NUnit.Tests
{
    [TestFixture]
    class NewBudgetTest
    {
        [Test]
        public void NoBudget()
        {
            var queryBudget = new QueryBudget();
            
            var actual = queryBudget.query(new DateTime(2024, 4, 20), new DateTime(2024, 4, 21));

            Assert.AreEqual(0, actual);
        }
    }
}
