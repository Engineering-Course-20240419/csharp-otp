using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_otp
{
    public class AuthenticationService
    {
        private ProfileDao profileDao;
        private RsaToken rsaToken;
        private Logger logger;

        public AuthenticationService(ProfileDao profileDao, RsaToken rsaToken, Logger logger)
        {
            this.profileDao = profileDao;
            this.rsaToken = rsaToken;
            this.logger = logger;
        }

        public bool IsValid(string userName, string password)
        {
            string passwordFromDao = profileDao.GetPassword(userName);
            string randomCode = rsaToken.GetRandom(userName);
            string validPassword = passwordFromDao + randomCode;

            bool isValid = password == validPassword;

            if (isValid)
            {
                return true;
            }
            else
            {
                logger.log("invalid login: " + userName);
                return false;
            }
        }
    }
}
