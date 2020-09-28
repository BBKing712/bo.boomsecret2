using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace bo.boomsecret.NoOpenSSL
{
    public static class Hashhelper
    {
        public static string Hash(string data, out byte[] hashBytes, Encoding encoding)
        {
            string key = null;
            byte[] hashBytes2 = null;
            string hash = Hashhelper.HashWithSHA2561(data, ref hashBytes2, encoding);
            key = hash.Substring(0, 32);
            hashBytes = new byte[32];
            hashBytes = encoding.GetBytes(key);


            return key;
        }

        public static string HashWithSHA2561(string data, ref byte[] hashBytes, Encoding encoding)
        {
            if (data == null)
            {
                return string.Empty;
            }

            byte[] dataBytes = encoding.GetBytes(data);
            using (SHA256Managed sha256 = new SHA256Managed())
            {
                hashBytes = sha256.ComputeHash(dataBytes);
            }

            string hashString = string.Empty;
            foreach (byte x in hashBytes)
            {
                hashString += string.Format("{0:x2}", x);
            }

            return hashString;

        }




















    }
}

