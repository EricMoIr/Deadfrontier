using System.Collections.Generic;
using System.Text;

namespace RequestHelper
{
    public class Hasher
    {
        private static string sKeyGen = "y27bigaOAA1";
        public static string Hash(Dictionary<string, string> body)
        {
            string toHash = sKeyGen;
            foreach(KeyValuePair<string, string> entry in body)
            {
                toHash += entry.Value;
            }
            return CreateMD5(toHash);
        }
        private static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
