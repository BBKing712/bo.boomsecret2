using OpenSSL.Crypto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace bo.boomsecret.Common
{
    public static class EncryptHelper
    {


        //https://stackoverflow.com/questions/2201631/how-do-i-use-the-openssl-net-c-sharp-wrapper-to-encrypt-a-string-with-aes
        public static string EncryptWithOpenSSLCrypt(string plainText, byte[] hashBytes, byte[] ivBytes, Encoding encoding)
        {
            string encrypted = null;
            CipherContext aes = new CipherContext(Cipher.AES_256_CBC);
            byte[] plainTextBytes = encoding.GetBytes(plainText);
            byte[] encryptedBytes = aes.Encrypt(plainTextBytes, hashBytes, ivBytes);
            encrypted = encoding.GetString(encryptedBytes);
            return encrypted;
        }
        public static string EncryptNEW(string plainText, byte[] hashBytes, byte[] ivBytes, Encoding encoding)
        {
            byte[] encryptedBytes;
            string encrypted = null;
                        using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = hashBytes;
                aesAlg.IV = ivBytes;
                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encryptedBytes = msEncrypt.ToArray();
                    }
                }



            }

            CipherContext aes = new CipherContext(Cipher.AES_256_CBC);
            byte[] plainTextBytes = encoding.GetBytes(plainText);
            encrypted = encoding.GetString(encryptedBytes);
            return encrypted;
        }







    }
}
