using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_otp
{
    public class Today
    {
        public virtual DateTime GetToday()
        {
            return DateTime.Now;
        }
    }
}
