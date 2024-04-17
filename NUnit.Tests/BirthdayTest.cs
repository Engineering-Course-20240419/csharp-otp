using NUnit.Framework;
using csharp_otp;
using Moq;
using System;

namespace unit_test
{

    // class StubToday : Today
    // {
    //     public override DateTime GetToday()
    //     {
    //         return Today;
    //     }
    //
    //     public DateTime Today;
    // }

    [TestFixture]
    class BirthdayTest
    {

        [Test]
        public void is_birthday()
        {
            Mock<Today> stubToday = new Mock<Today>();
            // stubToday.Today = new DateTime(2024, 4, 9);
            stubToday.Setup(t => t.GetToday()).Returns(new DateTime(2024, 4, 9));
            Birthday birthday = new Birthday(stubToday.Object);

            Assert.IsTrue(birthday.IsBirthday());
        }

        [Test]
        public void is_not_birthday()
        {
            Mock<Today> stubToday = new Mock<Today>();
            // stubToday.Today = new DateTime(2024, 5, 20);
            stubToday.Setup(t => t.GetToday()).Returns(new DateTime(2024, 5, 20));
            Birthday birthday = new Birthday(stubToday.Object);

            Assert.IsFalse(birthday.IsBirthday());
        }
    }
}
