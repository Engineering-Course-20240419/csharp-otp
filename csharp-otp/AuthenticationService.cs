using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_otp
{
    public class AuthenticationService
    {
        private readonly ProfileDao _profileDao;
        private readonly RsaToken _rsaToken;

        public AuthenticationService(ProfileDao profileDao, RsaToken rsaToken)
        {
            _profileDao = profileDao;
            _rsaToken = rsaToken;
        }

        public bool IsValid(string userName, string password)
        {
            string passwordFromDao = _profileDao.GetPassword(userName);
            string randomCode = _rsaToken.GetRandom(userName);
            string validPassword = passwordFromDao + randomCode;

            bool isValid = password == validPassword;

            if (isValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
