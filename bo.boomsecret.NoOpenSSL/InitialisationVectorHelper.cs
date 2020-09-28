using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace bo.boomsecret.NoOpenSSL
{
    public static class InitialisationVectorHelper
    {
        public static string CreateInitialisationVextorString(int size, out byte[] ivBytes, Encoding encoding)
        {
            string iv = null;


            ivBytes = new byte[size];
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                random.NextBytes(ivBytes);
            }
            iv = encoding.GetString(ivBytes);



            return iv;
        }

    }
}
