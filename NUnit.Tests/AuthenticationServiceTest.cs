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
    public class AuthenticationServiceTest
    {
        Mock<ProfileDao> stubProfileDao;
        Mock<RsaToken> stubRsaToken;
        Mock<Logger> mockLogger;
        AuthenticationService target;

        [SetUp]
        public void SetUp()
        {
            stubProfileDao = new Mock<ProfileDao>();
            stubRsaToken = new Mock<RsaToken>();
            mockLogger = new Mock<Logger>();
            target = new AuthenticationService(stubProfileDao.Object, stubRsaToken.Object, mockLogger.Object);
        }

        [Test]
        public void is_valid()
        {
            GivenPassword("91");
            GivenToken("000000");

            bool actual = target.IsValid("joey", "91000000");

            Assert.IsTrue(actual);
        }

        private void GivenToken(string token)
        {
            stubRsaToken.Setup(r => r.GetRandom(It.IsAny<string>())).Returns(token);
        }

        private void GivenPassword(string password)
        {
            stubProfileDao.Setup(p => p.GetPassword(It.IsAny<string>())).Returns(password);
        }

        [Test]
        public void is_not_valid()
        {
            GivenPassword("91");
            GivenToken("123456");

            bool actual = target.IsValid("joey", "91000000");

            Assert.IsFalse(actual);
        }

        [Test]
        public void log_failed_message()
        {
            GivenPassword("91");
            GivenToken("123456");

            target.IsValid("joey", "91000000");

            mockLogger.Verify(l => l.log("login failed joey"));
        }
    }
}
