using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_otp_2019
{
    public class PublicStaticMember
    {

        public PublicStaticMember(string text)
        {
            // Console.WriteLine(text);
            throw new Exception();
        }

        public PublicStaticMember()
        {
            Console.WriteLine("no exception construction");
        }

        public void Log()
        {
            Console.WriteLine("do something");
        }
    }
}
