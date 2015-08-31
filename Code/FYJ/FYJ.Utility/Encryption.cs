using System;
using System.Security.Cryptography;

namespace FYJ.Utility
{
    public class Encryption
    {
        /// <summary>
        /// Create sha256 with byte
        /// </summary>
        /// <param name="hashByte"></param>
        /// <returns></returns>
        public byte[] CreateSHA256Hash(byte[] hashByte)
        {
            SHA256 mySHA256 = SHA256Managed.Create();
            return mySHA256.ComputeHash(hashByte);
        }

        /// <summary>
        /// Create sha256 with string
        /// </summary>
        /// <param name="hashString"></param>
        /// <returns></returns>
        public byte[] CreateSHA256Hash(string hashString)
        {
            byte[] hashByte = System.Text.Encoding.UTF8.GetBytes(hashString);
            SHA256 mySHA256 = SHA256Managed.Create();
            return mySHA256.ComputeHash(hashByte);
        }

        /// <summary>
        /// Create random salt
        /// </summary>
        /// <param name="saltByteCount"></param>
        /// <returns></returns>
        public string GetRandomSalt(int saltByteCount)
        {
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            byte[] saltByte = new byte[saltByteCount];
            csprng.GetBytes(saltByte);
            return Convert.ToBase64String(saltByte);
        }
    }
}
