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

        [Test]
        public void GetIns_All_Poser()
        {
            var isValidShim = Pose.Shim.Replace(() => Pose.Is.A<AuthenticationService>().IsValid(Pose.Is.A<string>(), Pose.Is.A<string>()))
                .With(delegate(AuthenticationService @this, string userName, string password) { return true; });
            var getInsShim = Pose.Shim.Replace(() => AuthenticationService.GetIns()).With(() => new AuthenticationService());
            bool actual = false;
            Pose.PoseContext.Isolate(() =>
            {
                var authenticationService = AuthenticationService.GetIns();
                actual = authenticationService.IsValid("joey", "91000000");
            }, isValidShim, getInsShim);

            Assert.IsTrue(actual);
        }
    }
}