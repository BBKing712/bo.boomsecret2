using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace bo.boomsecret.Common
{
    public static class HmacHelper
    {
        public static string HashHmacWithOpenSSLSHA256(string encrypted, byte[] keyBytes, Encoding encoding)
        {
            using (HMACSHA256 hmacsha256 = new HMACSHA256(keyBytes))
            {
                hmacsha256.ComputeHash(encoding.GetBytes(encrypted));

                return string.Concat(Array.ConvertAll(hmacsha256.Hash, b => b.ToString("X2"))).ToLower();

            }

        }

    }
}
