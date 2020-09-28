using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace bo.boomsecret.Common
{
    public static class InitialisationVectorHelper
    {
        public static string CreateInitialisationVextorStringWithOpenSSL(int size, out byte[] ivBytes, Encoding encoding )
        {
            string iv = null;

            do
            {
                OpenSSL.Core.Random.Cleanup();
                ivBytes = OpenSSL.Core.Random.PseudoBytes(size);
                iv = Encoding.UTF8.GetString(ivBytes);
                iv = encoding.GetString(ivBytes);
            } while ((!string.IsNullOrEmpty(iv)) && (iv.Length != size));


            return iv;
        }
        public static string CreateInitialisationVextorStringNEW(int size, out byte[] ivBytes, Encoding encoding)
        {
            string iv = null;

            //var provider = new AesCryptoServiceProvider();
            //var xxx = provider.IV;

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
