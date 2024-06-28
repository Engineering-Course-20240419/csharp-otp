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
        private static AuthenticationService _instance;

        public AuthenticationService(ProfileDao profileDao, RsaToken rsaToken, Logger logger)
        {
            _profileDao = profileDao;
            _rsaToken = rsaToken;
            _logger = logger;
        }

        public AuthenticationService() : this(new ProfileDao(), new RsaToken(), new Logger())
        {
        }

        public static AuthenticationService GetIns()
        {
            // if (_instance == null)
            // {
            //     _instance = new AuthenticationService();
            // }

            return _instance;
        }

        public virtual bool IsValid(string userName, string password)
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
