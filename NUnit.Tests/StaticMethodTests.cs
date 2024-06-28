using System;
using csharp_otp;
using Moq;
using NUnit.Framework;

namespace NUnit.Tests
{
    [TestFixture]
    public class StaticMethodTests
    {
        [Test]
        public void GetIns_Moq_Poser()
        {
            var mock = new Mock<AuthenticationService>();
            mock.Setup(x => x.IsValid(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var shim = Pose.Shim.Replace(() => AuthenticationService.GetIns()).With(() => mock.Object);

            AuthenticationService authenticationService = null;

            Pose.PoseContext.Isolate(() =>
            {
                authenticationService = AuthenticationService.GetIns();
            }, shim);

            var actual = authenticationService.IsValid("joey", "91000000");

            Assert.IsTrue(actual);
        }
    }
}