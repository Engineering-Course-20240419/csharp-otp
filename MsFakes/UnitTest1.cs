using csharp_otp;
using csharp_otp.Fakes;
using csharp_otp_2019;
using csharp_otp_2019.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Runtime.Remoting.Messaging;

namespace MsFakes
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetIns_All_MsFakes()
        {
            
            using (ShimsContext.Create())
            {
                ShimAuthenticationService.AllInstances.IsValidStringString = 
                    (AuthenticationService instance, string username, string password) => { 
                        return true; 
                    };
                ShimAuthenticationService.GetIns = () => new AuthenticationService();

                var actual = AuthenticationService.GetIns().IsValid("abc", "123");

                Assert.IsTrue(actual);
            }
        }
        
        [TestMethod]
        public void Mock_Interface_Poser()
        {
            var stubUser = new Mock<IUser>();
            stubUser.Setup(x => x.GetUsername()).Returns("hello");

            using (ShimsContext.Create())
            {
                ShimUserFactory.GetInstance = () => stubUser.Object;

                var actual = UserFactory.GetInstance().GetUsername();

                Assert.AreEqual("hello", actual);
            }
        }

    }
}
