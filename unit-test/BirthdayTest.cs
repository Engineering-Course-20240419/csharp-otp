using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using csharp_otp;
using Rhino.Mocks;

namespace unit_test
{
    [TestFixture]
    class BirthdayTest
    {

        [Test]
        public void is_birthday()
        {
            MockRepository mocks = new MockRepository();
            Today stubToday = mocks.Stub<Today>();
            Expect.Call(stubToday.GetToday()).Return(new DateTime(2024, 4, 9));
            Birthday birthday = new Birthday(stubToday);
            mocks.ReplayAll();

            bool actual = birthday.IsBirthday();

            Assert.IsTrue(actual);
        }

        [Test]
        public void is_not_birthday()
        {
            MockRepository mocks = new MockRepository();
            Today stubToday = mocks.Stub<Today>();
            Expect.Call(stubToday.GetToday()).Return(new DateTime(2024, 5, 20));
            Birthday birthday = new Birthday(stubToday);
            mocks.ReplayAll();

            bool actual = birthday.IsBirthday();

            Assert.IsFalse(actual);
        }

    }
}
