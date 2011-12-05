using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace VisitaJayaPerkasa.Security.Cryptography
{
    public static class SHA1
    {
        public static string HashString(string text, string salt)
        {
            SHA1Managed sha = null;
            try
            {
                sha = new SHA1Managed();
                return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(text + salt)));
            }
            finally
            {
                if (sha != null)
                {
                    sha.Clear();
                    sha = null;
                }
            }
        }
    }
}
