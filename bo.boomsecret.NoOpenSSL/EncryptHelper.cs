using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace bo.boomsecret.NoOpenSSL
{
    public static class EncryptHelper
    {


        public static string Encrypt(string plainText, byte[] hashBytes, byte[] ivBytes, Encoding encoding)
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

            encrypted = encoding.GetString(encryptedBytes);
            return encrypted;
        }







    }
}
