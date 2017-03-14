using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace AuraInventarioProto.App_Start {
    public class HashClass {
        public static string CreateHash(string password) {
            SHA256CryptoServiceProvider Sha256Obj = new SHA256CryptoServiceProvider();
            Byte[] ToHash = Encoding.UTF8.GetBytes(password);
            ToHash = Sha256Obj.ComputeHash(ToHash);
            string result = String.Empty;

            foreach (Byte b in ToHash) {
                result += b.ToString("x2");
            }
            return result.Trim();
        }

        public static int CompareHash(string Current, string ToCompare) {
            int result = 0;
            if (Current.Trim() == ToCompare.Trim()) {
                result = 1;
            } else {
                result = 0;
            }
            return result;
        }

        public static string CreateSalt() {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[32];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        //public string ComputeHash(string input, HashAlgorithm algorithm) {
        //    Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

        //    Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

        //    return BitConverter.ToString(hashedBytes);
        //}
    }
}