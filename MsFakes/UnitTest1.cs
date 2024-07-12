using csharp_otp;
using csharp_otp.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}
