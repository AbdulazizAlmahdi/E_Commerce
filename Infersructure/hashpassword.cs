using System;

namespace E_commerce.Infersructure
{
    public static class Hashpassword
    {

        public static string Hashedpassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
            // return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public static bool Verify(string password, string hashedpassword)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hashedpassword);
            }
            catch (Exception e)
            {
                var exception = e.InnerException.Message;
                return false;

            }


        }
    }
}
