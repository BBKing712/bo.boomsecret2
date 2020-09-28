using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace bo.boomsecret.Common
{
    public static class BoomEncryptor
    {
        private static string _salt = "7zTmg5efWD9rQCN8zOWKWVIhXgBbHCeriJM10BQlMenefMcry9";

        private static Encoding _encoding = System.Text.ASCIIEncoding.Default;



        public static string GetEncryptedString(string email, string secret)
        {
            string encrypted = null;
            string tobeencrypted = (email + "|" + BoomEncryptor.GetUnixTimeNow().ToString());
            encrypted = BoomEncryptor.Encrypt(tobeencrypted, secret, _salt);

            return encrypted;
        }

        public static long GetUnixTimeNow()
        {
            TimeSpan timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }




        public static string Encrypt(string plain, string sectret, string salt)
        {
            string result = null;
            //Schlüssel für crypt und hmac erzeugen
            byte[] hashBytes = null;
            string hash = Hashhelper.Hash((sectret + salt), out hashBytes, _encoding);
            

            // IV erzeugen 16 bytes lang
            int ivSize = 16;
            byte[] ivBytes = null;
            string iv = InitialisationVectorHelper.CreateInitialisationVextorStringWithOpenSSL(ivSize, out ivBytes, _encoding);
            // Verschlüsseln
            string encrypted = EncryptHelper.EncryptWithOpenSSLCrypt(plain, hashBytes, ivBytes, _encoding);

            string ciphertext = iv + encrypted;
            // HMAC erzeugen
            string hmac = HmacHelper.HashHmacWithOpenSSLSHA256(ciphertext, hashBytes, _encoding);



            //zusammenfügen dann zu hex umwandeln und fertig
            byte[] resultBytes = _encoding.GetBytes(hmac + ciphertext);
            result = HexHelper.GetHexFromBytes(resultBytes);

            return result;
        }












    }
}
