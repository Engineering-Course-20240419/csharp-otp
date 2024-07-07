using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_otp_2019
{
    public class UserFactory
    {
        public class User : IUser
        {
            public string GetUsername()
            {
                throw new NotImplementedException();
            }
        }
        public static IUser GetInstance()
        {
            return new User();
        }
    }
}
