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
        public void setup()
        {
            stubToday = new Mock<Today>();
            birthday = new Birthday(stubToday.Object);
        }

        [Test]
        public void is_birthday()
        {
            GivenToday(4, 9);

            Assert.IsTrue(birthday.IsBirthday());
        }

        private void GivenToday(int month, int day)
        {
            stubToday.Setup(t => t.GetToday()).Returns(new DateTime(2024, month, day));
        }

        [Test]
        public void is_not_birthday()
        {
            GivenToday(5, 20);

            Assert.IsFalse(birthday.IsBirthday());
        }
    }
}
