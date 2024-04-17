using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_otp
{
    public class Birthday
    {
        private Today today;
        public Birthday(Today today)
        {
            this.today = today;
        }

        public bool IsBirthday()
        {
            DateTime now = today.GetToday();
            return now.Month == 4 && now.Day == 9;
        }
    }
}
