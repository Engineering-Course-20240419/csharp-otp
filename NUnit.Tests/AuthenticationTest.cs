using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using csharp_otp;
using csharp_otp_2019;
using Moq;
using NUnit.Framework;

namespace NUnit.Tests
{

    // class StubProfileDao : ProfileDao
    // {
    //     public override string GetPassword(string userName)
    //     {
    //         return Password;
    //     }
    //
    //     public string Password;
    // }

    // class StubRsaToken : RsaToken
    // {
    //     public override string GetRandom(string userName)
    //     {
    //         return Token;
    //     }
    //
    //     public string Token;
    // }

    // class MockLogger : Logger
    // {
    //     public override void log(string message)
    //     {
    //         _message = message;
    //     }
    //
    //     public string _message;
    // }

    [TestFixture]
    class AuthenticationTest
    {
        [Test]
        public void IsValid()
        {
            Mock<ProfileDao> stubProfileDao = new Mock<ProfileDao>();
            // stubProfileDao.Password = "91";
            stubProfileDao.Setup(p => p.GetPassword(It.IsAny<string>())).Returns("91");
            Mock<RsaToken> stubRsaToken = new Mock<RsaToken>();
            // stubRsaToken.Token = "000000";
            stubRsaToken.Setup(r => r.GetRandom(It.IsAny<string>())).Returns("000000");
            Logger logger = new Logger();
            AuthenticationService target = new AuthenticationService(stubProfileDao.Object, stubRsaToken.Object, logger);

            bool actual = target.IsValid("joey", "91000000");

            Assert.IsTrue(actual);
        }

        [Test]
        public void IsNotValid()
        {
            Mock<ProfileDao> stubProfileDao = new Mock<ProfileDao>();
            // stubProfileDao.Password = "91";
            stubProfileDao.Setup(p => p.GetPassword(It.IsAny<string>())).Returns("91");
            Mock<RsaToken> stubRsaToken = new Mock<RsaToken>();
            // stubRsaToken.Token = "123456";
            stubRsaToken.Setup(r => r.GetRandom(It.IsAny<string>())).Returns("123456");
            Logger logger = new Logger();
            AuthenticationService target = new AuthenticationService(stubProfileDao.Object, stubRsaToken.Object, logger);

            bool actual = target.IsValid("joey", "91000000");

            Assert.IsFalse(actual);
        }

        [Test]
        public void Log()
        {
            Mock<ProfileDao> stubProfileDao = new Mock<ProfileDao>();
            // stubProfileDao.Password = "91";
            stubProfileDao.Setup(p => p.GetPassword(It.IsAny<string>())).Returns("91");
            Mock<RsaToken> stubRsaToken = new Mock<RsaToken>();
            // stubRsaToken.Token = "123456";
            stubRsaToken.Setup(r => r.GetRandom(It.IsAny<string>())).Returns("123456");
            Mock<Logger> mockLogger = new Mock<Logger>();
            AuthenticationService target = new AuthenticationService(stubProfileDao.Object, stubRsaToken.Object, mockLogger.Object);

            target.IsValid("joey", "91000000");

            // Assert.AreEqual("invalid login: joey", mockLogger._message);
            mockLogger.Verify(l => l.log("invalid login: joey"));
        }
    }
}
