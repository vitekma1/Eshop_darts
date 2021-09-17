using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshop_projekt.Class
{
    public class PwHelper
    {
        public static string DecryptString(string encrString)
        {
            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(encrString);
                decrypted = System.Text.UTF8Encoding.UTF8.GetString(b);

            }
            catch
            {
                decrypted = "";
            }
            return decrypted;
        }
        public static string EncryptString(string strEncrypted)
        {
            byte[] b = System.Text.UTF8Encoding.UTF8.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }
    }
}