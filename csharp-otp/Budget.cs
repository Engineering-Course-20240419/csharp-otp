using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_otp
{
    public class Budget
    {
        public string YearMonth;
        public int Amount;

        public DateTime GetStart()
        {
            return DateTime.Parse(YearMonth + "-01");
        }

        public DateTime GetEnd()
        {
            return GetStart().AddMonths(1).AddDays(-1);
        }
    }
}
