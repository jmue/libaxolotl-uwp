﻿using System;
using System.Collections.Generic;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace libaxolotl.util
{
    public class Sign
    {
        public static byte[] sha256sum(byte[] key, byte[] message)
        {
            MacAlgorithmProvider provider = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacSha256);

            IBuffer buffKey = CryptographicBuffer.CreateFromByteArray(key);
            CryptographicKey hmacKey = provider.CreateKey(buffKey);

            IBuffer buffMessage = CryptographicBuffer.CreateFromByteArray(message);

            IBuffer buffHMAC = CryptographicEngine.Sign(hmacKey, buffMessage);

            byte[] hmac;

            CryptographicBuffer.CopyToByteArray(buffHMAC, out hmac);

            return hmac;
        }
    }

    public class Encrypt
    {
        public static byte[] aesCbcPkcs5(byte[] message, byte[] key, byte[] iv)
        {
            SymmetricKeyAlgorithmProvider objAlg = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesCbcPkcs7); // PKCS5
            IBuffer buffKey = CryptographicBuffer.CreateFromByteArray(key);
            CryptographicKey ckey = objAlg.CreateSymmetricKey(buffKey);


            IBuffer buffPlaintext = CryptographicBuffer.CreateFromByteArray(message);
            IBuffer buffIV = CryptographicBuffer.CreateFromByteArray(iv);
            IBuffer buffEncrypt = CryptographicEngine.Encrypt(ckey, buffPlaintext, buffIV);

            byte[] ret;
            CryptographicBuffer.CopyToByteArray(buffEncrypt, out ret);

            return ret;
        }

        public static byte[] aesCtr(byte[] message, byte[] key, uint counter)
        {
            SymmetricKeyAlgorithmProvider objAlg = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesCbcPkcs7); // CRT
            IBuffer buffKey = CryptographicBuffer.CreateFromByteArray(key);
            CryptographicKey ckey = objAlg.CreateSymmetricKey(buffKey);

            byte[] ivBytes = new byte[16];
            ByteUtil.intToByteArray(ivBytes, 0, (int)counter);

            IBuffer buffPlaintext = CryptographicBuffer.CreateFromByteArray(message);
            IBuffer buffIV = CryptographicBuffer.CreateFromByteArray(ivBytes);
            IBuffer buffEncrypt = CryptographicEngine.Encrypt(ckey, buffPlaintext, buffIV);

            byte[] ret;
            CryptographicBuffer.CopyToByteArray(buffEncrypt, out ret);

            return ret;
        }
    }

    public class Decrypt
    {
        public static byte[] aesCbcPkcs5(byte[] message, byte[] key, byte[] iv)
        {
            SymmetricKeyAlgorithmProvider objAlg = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesCbcPkcs7);
            IBuffer buffKey = CryptographicBuffer.CreateFromByteArray(key);
            CryptographicKey ckey = objAlg.CreateSymmetricKey(buffKey);


            IBuffer buffPlaintext = CryptographicBuffer.CreateFromByteArray(message);
            IBuffer buffIV = CryptographicBuffer.CreateFromByteArray(iv);
            IBuffer buffEncrypt = CryptographicEngine.Decrypt(ckey, buffPlaintext, buffIV);

            byte[] ret;
            CryptographicBuffer.CopyToByteArray(buffEncrypt, out ret);
            return ret;
        }

        public static byte[] aesCtr(byte[] message, byte[] key, uint counter)
        {
            SymmetricKeyAlgorithmProvider objAlg = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesCbcPkcs7);
            IBuffer buffKey = CryptographicBuffer.CreateFromByteArray(key);
            CryptographicKey ckey = objAlg.CreateSymmetricKey(buffKey);

            byte[] ivBytes = new byte[16];
            ByteUtil.intToByteArray(ivBytes, 0, (int)counter);

            IBuffer buffPlaintext = CryptographicBuffer.CreateFromByteArray(message);
            IBuffer buffIV = CryptographicBuffer.CreateFromByteArray(ivBytes);
            IBuffer buffEncrypt = CryptographicEngine.Decrypt(ckey, buffPlaintext, buffIV);

            byte[] ret;
            CryptographicBuffer.CopyToByteArray(buffEncrypt, out ret);
            return ret;
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
