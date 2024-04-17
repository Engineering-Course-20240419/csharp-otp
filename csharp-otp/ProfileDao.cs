using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_otp
{
    public class ProfileDao
    {
        public virtual string GetPassword(string userName)
        {
            return profiles[userName];
        }

        private Dictionary<string, string> profiles = new Dictionary<string, string>();
    }
}
