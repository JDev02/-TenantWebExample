using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TenantWebExample
{
    public class CookieBase64Cryptography : ICookieCryptography
    {
        public string Decrypt(string encryptText)
        {
            return ProcessEncryptText(encryptText);
        }

        public string Encrypt(string text)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
        }

        private string ProcessEncryptText(string encryptText)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(encryptText));
        }
    }
}