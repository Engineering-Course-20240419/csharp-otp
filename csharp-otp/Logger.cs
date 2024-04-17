using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_otp
{
    public class Logger
    {
        public virtual void log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
