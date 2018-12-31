using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Nagarro.Gitter.Shared
{
    
    public class PasswordHelper
    {
        public static string HashPassword(string password)
        {

            return BCrypt.Net.BCrypt.HashPassword(password, 10);
        }

        public static bool VerifyPassword(string passwordToCheck, string hashToMatch)
        {
            return BCrypt.Net.BCrypt.Verify(passwordToCheck, hashToMatch);
        }
    }
}