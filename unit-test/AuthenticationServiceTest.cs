using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using csharp_otp;
using Rhino.Mocks;

namespace unit_test
{
    public class MockLogger : Logger
    {
        public override void log(string message)
        {
            _message = message;
        }
        public string _message;
    }

    [TestFixture]
    class AuthenticationServiceTest
    {

        [Test]
        public void is_valid()
        {
            MockRepository mocks = new MockRepository();
            ProfileDao stubProfileDao = mocks.Stub<ProfileDao>();
            Expect.Call(stubProfileDao.GetPassword(Arg<string>.Is.Anything)).Return("91"); 
            RsaToken stubRsaToken = mocks.Stub<RsaToken>();
            Expect.Call(stubRsaToken.GetRandom(Arg<string>.Is.Anything)).Return("000000");
            Logger mockLogger = mocks.DynamicMock<Logger>();
            AuthenticationService target = new AuthenticationService(stubProfileDao, stubRsaToken, mockLogger);
            mocks.ReplayAll();

            bool actual = target.IsValid("joey", "91000000");

            Assert.IsTrue(actual);
        }

        [Test]
        public void is_not_valid()
        {
            MockRepository mocks = new MockRepository();
            ProfileDao stubProfileDao = mocks.Stub<ProfileDao>();
            Expect.Call(stubProfileDao.GetPassword(Arg<string>.Is.Anything)).Return("91");
            RsaToken stubRsaToken = mocks.Stub<RsaToken>();
            Expect.Call(stubRsaToken.GetRandom(Arg<string>.Is.Anything)).Return("123456");
            Logger mockLogger = mocks.DynamicMock<Logger>();
            AuthenticationService target = new AuthenticationService(stubProfileDao, stubRsaToken, mockLogger);
            mocks.ReplayAll();

            bool actual = target.IsValid("joey", "91000000");

            Assert.IsFalse(actual);
        }

        [Test]
        public void log_failed_message()
        {
            MockRepository mocks = new MockRepository();
            ProfileDao stubProfileDao = mocks.Stub<ProfileDao>();
            Expect.Call(stubProfileDao.GetPassword(Arg<string>.Is.Anything)).Return("91");
            RsaToken stubRsaToken = mocks.Stub<RsaToken>();
            Expect.Call(stubRsaToken.GetRandom(Arg<string>.Is.Anything)).Return("123456");
            Logger mockLogger = mocks.DynamicMock<Logger>();
            Expect.Call(delegate { mockLogger.log("invalid login: joey"); });
            AuthenticationService target = new AuthenticationService(stubProfileDao, stubRsaToken, mockLogger);
            mocks.ReplayAll();

            target.IsValid("joey", "91000000");

            mocks.VerifyAll();
        }
    }
}
