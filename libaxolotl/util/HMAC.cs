using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace libaxolotl.util
{
    public class Sign
    {
        public static byte[] sha256sum(byte[] key, byte[] message)
        {
            HMAC hash = new HMACSHA256(key);
            return hash.ComputeHash(message);
        }
    }

    public class Encrypt
    {
        public static byte[] aesCbcPkcs5(byte[] message, byte[] key, byte[] iv)
        {
            byte[] encrypted;

            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.BlockSize = 128;
                aesAlg.KeySize = 256;
                aesAlg.Padding = PaddingMode.PKCS7;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Key = key;
                aesAlg.IV = iv;

                using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                {
                    using (MemoryStream to = new MemoryStream())
                    {
                        using (CryptoStream writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
                        {
                            writer.Write(message, 0, message.Length);
                            writer.FlushFinalBlock();
                            encrypted = to.ToArray();
                        }
                    }
                }
            }

            //return ByteUtil.trim(encrypted, message.Length);
            return encrypted;
        }

        public static byte[] aesCtr(byte[] message, byte[] key, uint counter)
        {
            byte[] ivBytes = new byte[16];
            ByteUtil.intToByteArray(ivBytes, 0, (int)counter);

            return aesCbcPkcs5(message, key, ivBytes);
        }
    }

    public class Decrypt
    {
        public static byte[] aesCbcPkcs5(byte[] message, byte[] key, byte[] iv)
        {
            byte[] decrypted;
            int decryptedByteCount = 0;

            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.BlockSize = 128;
                aesAlg.KeySize = 256;
                aesAlg.Padding = PaddingMode.PKCS7;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Key = key;
                aesAlg.IV = iv;

                using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                {
                    using (MemoryStream from = new MemoryStream(message))
                    {
                        using (CryptoStream reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read))
                        {
                            decrypted = new byte[message.Length];
                            decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
                        }
                    }
                }

            }

            return ByteUtil.trim(decrypted, decryptedByteCount);
        }

        public static byte[] aesCtr(byte[] message, byte[] key, uint counter)
        {
            byte[] ivBytes = new byte[16];
            ByteUtil.intToByteArray(ivBytes, 0, (int)counter);

            return aesCbcPkcs5(message, key, ivBytes);
        }
    }

    public static class CryptoHelper
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
