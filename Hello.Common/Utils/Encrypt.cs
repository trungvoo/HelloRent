using System;
using System.Security.Cryptography;
using System.Text;

namespace Hello.Common.Utils
{
    public class Encrypt
    {
        public static string MD5Encrypt(string toEncrypt)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(toEncrypt));

            byte[] result = md5.Hash;

            StringBuilder str = new StringBuilder();
            for (int i = 1; i < result.Length; i++)
                str.Append(result[i].ToString("x2"));

            return str.ToString();
        }

        public static string md5(string str)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(str);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }

            return s.ToString();
        }
    }
}
