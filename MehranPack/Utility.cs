using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MehranPack
{
    public class Utility
    {
        public static DateTime AdjustTimeOfDate(DateTime input)
        {
            TimeSpan ts = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            input += ts;
            return input;
        }

        private static string AesKey = "axH//yJ3zKqImVUb//9Vu7uqmZm7/8xn";
        public static string AesEncrypt(string data)
        {
            byte[] toEncryptArry = Encoding.UTF8.GetBytes(data);
            byte[] keyArry = Convert.FromBase64String(AesKey);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider
            {
                Key = keyArry,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.ISO10126
            };
            ICryptoTransform cTransform = aes.CreateEncryptor();
            byte[] encrypted = cTransform.TransformFinalBlock(toEncryptArry, 0, toEncryptArry.Length);
            string toBase64 = Convert.ToBase64String(encrypted);
            return toBase64;
        }

        public static string AesDecrypt(string data)
        {
            byte[] toDecryptArry = Convert.FromBase64String(data);
            byte[] keyArry = Convert.FromBase64String(AesKey);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider
            {
                Key = keyArry,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.ISO10126
            };
            ICryptoTransform cTransform = aes.CreateDecryptor();
            byte[] decrypted = cTransform.TransformFinalBlock(toDecryptArry, 0, toDecryptArry.Length);
            string ut8String = Encoding.UTF8.GetString(decrypted);
            return ut8String;
        }

        public static bool IsInOutMode()
        {
            return ((Repository.Entity.Domain.User)HttpContext.Current.Session["User"])?.Username.ToLower() == "inoutop";
        }
    }
}