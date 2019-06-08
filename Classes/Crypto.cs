using System;
using System.Security.Cryptography;
using System.Text;

namespace MoS {
    public static class Crypto {
        #region SHA256
        private static String ByteArrayToHexString(byte[] ba) {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);

            return hex.ToString();
        }

        public static String CreateSalt(int length) {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[length];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

        public static String HashToSHA256(string password, string salt) {
            byte[] bytes = Encoding.UTF8.GetBytes(password + salt);
            SHA256Managed sha256hashstring = new SHA256Managed();

            byte[] hash = sha256hashstring.ComputeHash(bytes);

            return ByteArrayToHexString(hash);
        }
        #endregion
    }
}
