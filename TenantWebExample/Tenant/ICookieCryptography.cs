using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TenantWebExample
{
    public interface ICookieCryptography
    {
        string Decrypt(string encryptText);
        string Encrypt(string text);
    }
}