using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace StudyProject.Models
{
    public static class HashGenerator
    {

        public static byte[] HashBin(string data, string salt)
        {
            string plain = data + salt;
            using (SHA512 shaM = new SHA512Managed())
            {
                byte[] dataHash = shaM.ComputeHash(Encoding.UTF8.GetBytes(plain));
                byte[] SaltHash = shaM.ComputeHash(Encoding.UTF8.GetBytes(salt));
                byte[] Sumus = new byte[dataHash.Length + SaltHash.Length];
                System.Array.Copy(dataHash, 0, Sumus, 0, dataHash.Length);
                System.Array.Copy(dataHash, 0, Sumus, dataHash.Length, SaltHash.Length);
                dataHash = shaM.ComputeHash(Sumus);
                for (int i = 0; i < 1000; i++) //Lets be veeeeery slow!
                {
                    SaltHash = shaM.ComputeHash(Encoding.UTF8.GetBytes(SaltHash.ToString() + i.ToString()));
                    System.Array.Copy(dataHash, 0, Sumus, 0, dataHash.Length);
                    System.Array.Copy(dataHash, 0, Sumus, dataHash.Length, SaltHash.Length);
                    dataHash = shaM.ComputeHash(Sumus);
                }
                return dataHash;
            }
        }

        public static string BinToStr(byte[] hash)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                sb.AppendFormat("{0:x2}", hash[i]);
            return sb.ToString().ToLower();
        }
        public static string HashStr(string data, string salt)
        {
            return BinToStr(HashBin(data, salt));
        }
    }
}