using System.Web;

namespace BankNet.Core
{
    public class Security
    {
        public static string GetMD5Hash(string input)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            string password = s.ToString();
            return password;
        }

        //public static string MD5(string input)
        //{
        //    CheckMD5 md5 = new CheckMD5();
        //    return md5.getMd5Hash(input);
        //}

        public static bool AllowCall(HttpContext context)
        {
            string ServerLocal = context.Request.Url.Authority;
            string ServerRefeffer = "";
            if (context.Request.UrlReferrer != null) ServerRefeffer = context.Request.UrlReferrer.Authority;
            return (ServerLocal == ServerRefeffer);
        }
    }
}
