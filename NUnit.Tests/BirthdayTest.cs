using NUnit.Framework;
using csharp_otp;
using Moq;
using System;

namespace unit_test
{
    [TestFixture]
    class BirthdayTest
    {
        Mock<Today> stubToday;
        Birthday birthday;

        [SetUp]
        public void SetUp()
        {
            stubToday = new Mock<Today>();
            birthday = new Birthday(stubToday.Object);
        }

        [Test]
        public void is_birthday()
        {
            GivenToday(new DateTime(2024, 4, 9));

            Assert.IsTrue(birthday.IsBirthday());
        }

        [Test]
        public void is_not_birthday()
        {
            GivenToday(new DateTime(2024, 5, 20));

            Assert.IsFalse(birthday.IsBirthday());
        }

        private void GivenToday(DateTime today)
        {
            stubToday.Setup(t => t.GetToday()).Returns(today);
        }

    }
}
