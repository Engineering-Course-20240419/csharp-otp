using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_otp_2019
{
    public class Logger
    {
        public virtual void log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
