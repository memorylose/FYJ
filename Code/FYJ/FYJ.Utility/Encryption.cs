using System;
using System.Security.Cryptography;

namespace FYJ.Utility
{
    public class Encryption
    {
        /// <summary>
        /// Create byte sha256
        /// </summary>
        /// <param name="hashByte"></param>
        /// <returns></returns>
        public static byte[] CreateSHA256HashByte(byte[] hashByte)
        {
            SHA256 mySHA256 = SHA256Managed.Create();
            return mySHA256.ComputeHash(hashByte);
        }

        /// <summary>
        /// Create byte sha256
        /// </summary>
        /// <param name="hashString"></param>
        /// <returns></returns>
        public static byte[] CreateSHA256HashByte(string hashString)
        {
            byte[] hashByte = System.Text.Encoding.UTF8.GetBytes(hashString);
            SHA256 mySHA256 = SHA256Managed.Create();
            return mySHA256.ComputeHash(hashByte);
        }

        /// <summary>
        /// Create string sha256 
        /// </summary>
        /// <param name="hashString"></param>
        /// <returns></returns>
        public static string CreateSHA256HashString(string hashString)
        {
            byte[] hashByte = System.Text.Encoding.UTF8.GetBytes(hashString);
            SHA256 mySHA256 = SHA256Managed.Create();
            return Convert.ToBase64String(mySHA256.ComputeHash(hashByte));
        }

        /// <summary>
        /// Create random salt
        /// </summary>
        /// <param name="saltByteCount"></param>
        /// <returns></returns>
        public static string GetRandomSalt(int saltByteCount)
        {
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            byte[] saltByte = new byte[saltByteCount];
            csprng.GetBytes(saltByte);
            return Convert.ToBase64String(saltByte);
        }
    }
}
