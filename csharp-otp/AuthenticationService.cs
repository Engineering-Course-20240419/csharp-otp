using System;
using System.Collections.Generic;
using System.Text;
using csharp_otp_2019;

namespace csharp_otp
{
    public class AuthenticationService
    {
        private readonly ProfileDao _profileDao;
        private readonly RsaToken _rsaToken;
        private readonly Logger _logger;

        public AuthenticationService(ProfileDao profileDao, RsaToken rsaToken, Logger logger)
        {
            _profileDao = profileDao;
            _rsaToken = rsaToken;
            _logger = logger;
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
                _logger.log("invalid login: " + userName);
                return false;
            }
        }
    }
}
