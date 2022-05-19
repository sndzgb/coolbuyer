using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.Security.DataProtection
{
    public class AesEncryption : IDataProtector
    {
        private readonly string encryptionKey;


        public AesEncryption()
        {
            // inject enc key!!!!
            encryptionKey = "jqc2D7xejEYxSEVCHFv8igJFhTzI7v9y";
        }


        public string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                throw new ArgumentNullException("plainText");
            }

            using (var aes = new AesCryptoServiceProvider()
            {
                Key = Encoding.UTF8.GetBytes(encryptionKey),
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            })
            {
                byte[] input = Encoding.UTF8.GetBytes(plainText);
                aes.GenerateIV();
                var iv = aes.IV;
                using (var encrypter = aes.CreateEncryptor(aes.Key, iv))
                using (var cipherStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(cipherStream, encrypter, CryptoStreamMode.Write))
                    using (var binaryWriter = new BinaryWriter(cryptoStream))
                    {
                        cipherStream.Write(iv, 0, iv.Length);
                        binaryWriter.Write(input);
                        cryptoStream.FlushFinalBlock();
                    }

                    string encryptedString = Convert.ToBase64String(cipherStream.ToArray());

                    return encryptedString;
                }
            }
        }

        public string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
            {
                throw new ArgumentNullException("cipherText");
            }

            //int ivSize = 16; // aes default
            byte[] payload = Convert.FromBase64String(cipherText);

            var aes = new AesCryptoServiceProvider()
            {
                Key = Encoding.UTF8.GetBytes(encryptionKey),
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };

            var iv = new byte[16];
            Array.Copy(payload, 0, iv, 0, iv.Length);

            using (var memoryStream = new MemoryStream())
            {
                using (var cs = new CryptoStream(memoryStream, aes.CreateDecryptor(aes.Key, iv), CryptoStreamMode.Write))
                using (var binaryWriter = new BinaryWriter(cs))
                {
                    binaryWriter.Write(
                        payload,
                        iv.Length,
                        payload.Length - iv.Length
                    );
                }

                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }
    }
}
