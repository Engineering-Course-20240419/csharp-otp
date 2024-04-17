using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using csharp_otp;
using csharp_otp_2019;
using NUnit.Framework;

namespace NUnit.Tests
{

    class StubProfileDao : ProfileDao
    {
        public override string GetPassword(string userName)
        {
            return Password;
        }

        public string Password;
    }

    class StubRsaToken : RsaToken
    {
        public override string GetRandom(string userName)
        {
            return Token;
        }

        public string Token;
    }

    class MockLogger : Logger
    {
        public override void log(string message)
        {
            _message = message;
        }

        public string _message;
    }

    [TestFixture]
    class AuthenticationTest
    {
        [Test]
        public void IsValid()
        {
            StubProfileDao stubProfileDao = new StubProfileDao();
            stubProfileDao.Password = "91";
            StubRsaToken stubRsaToken = new StubRsaToken();
            stubRsaToken.Token = "000000";
            Logger logger = new Logger();
            AuthenticationService target = new AuthenticationService(stubProfileDao, stubRsaToken, logger);

            bool actual = target.IsValid("joey", "91000000");

            Assert.IsTrue(actual);
        }

        [Test]
        public void IsNotValid()
        {
            StubProfileDao stubProfileDao = new StubProfileDao();
            stubProfileDao.Password = "91";
            StubRsaToken stubRsaToken = new StubRsaToken();
            stubRsaToken.Token = "123456";
            Logger logger = new Logger();
            AuthenticationService target = new AuthenticationService(stubProfileDao, stubRsaToken, logger);

            bool actual = target.IsValid("joey", "91000000");

            Assert.IsFalse(actual);
        }

        [Test]
        public void Log()
        {
            StubProfileDao stubProfileDao = new StubProfileDao();
            stubProfileDao.Password = "91";
            StubRsaToken stubRsaToken = new StubRsaToken();
            stubRsaToken.Token = "123456";
            MockLogger mockLogger = new MockLogger();
            AuthenticationService target = new AuthenticationService(stubProfileDao, stubRsaToken, mockLogger);

            target.IsValid("joey", "91000000");

            Assert.AreEqual("invalid login: joey", mockLogger._message);
        }
    }
}
