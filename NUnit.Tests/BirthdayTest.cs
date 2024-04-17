using NUnit.Framework;
using csharp_otp;
using Moq;
using System;

namespace unit_test
{

    class StubToday : Today
    {
        public override DateTime GetToday()
        {
            return Today;
        }

        public DateTime Today;
    }

    [TestFixture]
    class BirthdayTest
    {

        [Test]
        public void is_birthday()
        {
            StubToday stubToday = new StubToday();
            stubToday.Today = new DateTime(2024, 4, 9);
            Birthday birthday = new Birthday(stubToday);

            Assert.IsTrue(birthday.IsBirthday());
        }

        [Test]
        public void is_not_birthday()
        {
            StubToday stubToday = new StubToday();
            stubToday.Today = new DateTime(2024, 5, 20);
            Birthday birthday = new Birthday(stubToday);

            Assert.IsFalse(birthday.IsBirthday());
        }
    }
}
