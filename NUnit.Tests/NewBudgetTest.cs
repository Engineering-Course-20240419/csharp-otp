using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using csharp_otp;
using csharp_otp_2019;
using Moq;

namespace NUnit.Tests
{
    [TestFixture]
    class NewBudgetTest
    {
        Mock<IBudgetRepo> stubRepo;
        QueryBudget queryBudget;

        [SetUp]
        public void setup()
        {
            stubRepo = new Mock<IBudgetRepo>();
            queryBudget = new QueryBudget(stubRepo.Object);
        }

        [Test]
        public void NoBudget()
        {
            GivenBudgets();

            var actual = queryBudget.query(
                new DateTime(2024, 4, 20), 
                new DateTime(2024, 4, 21));

            Assert.AreEqual(0, actual);
        }

        private void GivenBudgets(params Budget[] budgets)
        {
            stubRepo.Setup(r => r.GetAll()).Returns(budgets.ToList());
        }

        [Test]
        public void StartAfterBudgetStart()
        {
            GivenBudgets(new Budget
            {
                YearMonth = "2024-04",
                Amount = 30
            });
            
            var actual = queryBudget.query(
                new DateTime(2024, 4, 20), 
                new DateTime(2024, 4, 20));

            Assert.AreEqual(1, actual);
        }

        [Test]
        public void StartAfterBudgetStartMoreThanOneDay()
        {
            GivenBudgets(new Budget
            {
                YearMonth = "2024-04",
                Amount = 30
            });
            
            var actual = queryBudget.query(
                new DateTime(2024, 4, 20), 
                new DateTime(2024, 4, 21));

            Assert.AreEqual(2, actual);
        }
        
        [Test]
        public void StartBeforeBudgetStart()
        {
            GivenBudgets(new Budget
            {
                YearMonth = "2024-04",
                Amount = 30
            });
            
            var actual = queryBudget.query(
                new DateTime(2024, 3, 20), 
                new DateTime(2024, 4, 15));
        
            Assert.AreEqual(15, actual);
        }

        [Test]
        public void StartAfterBudgetEnd()
        {
            GivenBudgets(new Budget
            {
                YearMonth = "2024-04",
                Amount = 30
            });
            
            var actual = queryBudget.query(
                new DateTime(2024, 5, 1), 
                new DateTime(2024, 5, 15));
        
            Assert.AreEqual(0, actual);
        }
    }
}
