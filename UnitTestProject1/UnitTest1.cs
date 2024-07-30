using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using csharp_otp_2019;
using Microsoft.QualityTools.Testing.Fakes;
using csharp_otp_2019.Fakes;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            Console.WriteLine("assembly init");
            // Use reflection to change the value of a static field
            //FieldInfo fieldInfo = typeof(Dependency).GetField("PublicStaticMember", BindingFlags.Public | BindingFlags.Static);
            //if (fieldInfo != null)
            //{
            //    try
            //    {
            //        fieldInfo.SetValue(null, null);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("Failed to set value: " + ex.Message);
            //    }
            //}

            // Console.WriteLine("Dependency.PublicStaticMember: " + Dependency.PublicStaticMember);

            //using (ShimsContext.Create())
            //{
            //    ShimDependency.StaticConstructor = () => { };
            //}
        }

        [TestMethod]
        public void TestMethod1()
        {
            using (ShimsContext.Create())
            {
                ShimDependency.StaticConstructor = () => {
                    Dependency._PublicStaticMember = new PublicStaticMember();
                    Dependency.PublicStaticMember2 = new PublicStaticMember();
                };
                //StubDependency.PublicStaticMember = new PublicStaticMember();
                //Dependency.PublicStaticMember = new PublicStaticMember();
                new Sut().Action();
            }
        }
    }
}
