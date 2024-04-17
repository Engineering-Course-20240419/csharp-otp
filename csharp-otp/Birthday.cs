using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_otp
{
    public class Birthday
    {
        private readonly Today _today;

        public Birthday(Today today)
        {
            _today = today;
        }
        public bool IsBirthday()
        {
            DateTime now = _today.GetToday();
            return now.Month == 4 && now.Day == 9;
        }
    }

    public class Today
    {
        public virtual DateTime GetToday()
        {
            return DateTime.Now;
        }
    }
}
