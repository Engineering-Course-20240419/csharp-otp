using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_otp_2019
{
    public class Dependency
    {
        public static PublicStaticMember PublicStaticMember = new PublicStaticMember("first member created");

        public static PublicStaticMember PublicStaticMember2 = new PublicStaticMember("second member created");

        public void BeToTested()
        {

        }
    }
}
