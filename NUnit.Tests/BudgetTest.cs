using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using csharp_otp;
using Moq;

namespace NUnit.Tests
{
    [TestFixture]
    public class BudgetTest
    {
        Mock<IBudgetRepo> stubBudgetRepo;
        BudgetService target;

        [SetUp]
        public void Setup()
        {
            stubBudgetRepo = new Mock<IBudgetRepo>();
            target = new BudgetService(stubBudgetRepo.Object);
        }

        private void GivenBudgets(params Budget[] budgets)
        {
            var allBudgets = budgets.ToList();
            stubBudgetRepo.Setup(r => r.GetAll()).Returns(allBudgets);
        }

        [Test]
        public void StartIsAfterEnd()
        {
            GivenBudgets(new Budget { YearMonth = "202304", Amount = 30 });

            var actual = target.Query(new DateTime(2023, 4, 2), new DateTime(2023, 4, 1));

            Assert.AreEqual(0, actual);
        }

        [Test]
        public void NoBudget()
        {
            GivenBudgets();

            var actual = target.Query(new DateTime(2023, 4, 1), new DateTime(2023, 4, 2));

            Assert.AreEqual(0, actual);
        }

        [Test]
        public void StartIsBeforeBudgetStart()
        {
            GivenBudgets(new Budget { YearMonth = "202304", Amount = 30 });

            var actual = target.Query(new DateTime(2023, 3, 31), new DateTime(2023, 4, 2));

            Assert.AreEqual(2, actual);
        }

        [Test]
        public void StartIsBetweenBudgetStartAndEnd()
        {
            GivenBudgets(new Budget { YearMonth = "202304", Amount = 30 });

            var actual = target.Query(new DateTime(2023, 4, 2), new DateTime(2023, 4, 2));

            Assert.AreEqual(1, actual);
        }

        [Test]
        public void StartIsAfterBudgetEnd()
        {
            GivenBudgets(new Budget { YearMonth = "202304", Amount = 30 });

            var actual = target.Query(new DateTime(2023, 5, 1), new DateTime(2023, 5, 10));

            Assert.AreEqual(0, actual);
        }

        [Test]
        public void EndIsBeforeBudgetStart()
        {
            GivenBudgets(new Budget { YearMonth = "202304", Amount = 30 });

            var actual = target.Query(new DateTime(2023, 3, 20), new DateTime(2023, 3, 31));

            Assert.AreEqual(0, actual);
        }

        [Test]
        public void EndIsBetweenBudgetStartAndEnd()
        {
            GivenBudgets(new Budget { YearMonth = "202304", Amount = 30 });

            var actual = target.Query(new DateTime(2023, 4, 10), new DateTime(2023, 4, 15));

            Assert.AreEqual(6, actual);
        }

        [Test]
        public void EndIsAfterBudgetEnd()
        {
            GivenBudgets(new Budget { YearMonth = "202304", Amount = 30 });

            var actual = target.Query(new DateTime(2023, 4, 15), new DateTime(2023, 5, 5));

            Assert.AreEqual(16, actual);
        }

        [Test]
        public void TwoBudgetsContinuous()
        {
            GivenBudgets(new Budget { YearMonth = "202304", Amount = 30 }, new Budget { YearMonth = "202305", Amount = 31});

            var actual = target.Query(new DateTime(2023, 4, 15), new DateTime(2023, 5, 15));

            Assert.AreEqual(16 + 15, actual);
        }

        [Test]
        public void TwoBudgetsNotContinuous()
        {
            GivenBudgets(new Budget { YearMonth = "202304", Amount = 30 }, new Budget { YearMonth = "202306", Amount = 30});

            var actual = target.Query(new DateTime(2023, 3, 15), new DateTime(2023, 6, 15));

            Assert.AreEqual(30 + 15, actual);
        }

        [Test]
        public void DailyAmount()
        {
            GivenBudgets(new Budget { YearMonth = "202304", Amount = 600 });

            var actual = target.Query(new DateTime(2023, 4, 10), new DateTime(2023, 4, 15));

            Assert.AreEqual(600 / 30 * 6, actual);
        }

    }
}
