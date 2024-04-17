using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using csharp_otp;
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
            AuthenticationService target = new AuthenticationService(stubProfileDao, stubRsaToken);

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
            AuthenticationService target = new AuthenticationService(stubProfileDao, stubRsaToken);

            bool actual = target.IsValid("joey", "91000000");

            Assert.IsFalse(actual);
        }
    }
}
